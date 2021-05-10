using System;
using NLog.Web;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string file = "C:\\Users\\Ethan Computer\\Documents\\DotNet\\MovieLibrary\\ml-latest-small\\movies.csv";
            
            
            string path = Directory.GetCurrentDirectory() + "\\nlog.config";
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();
            logger.Info("Program Started");
             List<UInt64> Ids = new List<UInt64>();
             List<string> Titles = new List<string>();
             List<string> Genres = new List<string>();
             if (!File.Exists(file))
                  {
                      logger.Error("File doesn't exist: {File}", file);
                  }
       else
         {
          
           try
             {
                 StreamReader sr = new StreamReader(file);
                 sr.ReadLine();
                 while(!sr.EndOfStream)
                 {
                    string row = sr.ReadLine();
                    int indexmethod = row.IndexOf('"');
                    if(indexmethod == -1)
                    {
                        string[] entries = row.Split(',');
                        Ids.Add(UInt64.Parse(entries[0]));
                        Titles.Add(entries[1]);
                        Genres.Add(entries[2].Replace("|",","));
                    }
                    else
                    {
                          Ids.Add(UInt64.Parse(row.Substring(0, indexmethod - 1)));
                          row = row.Substring(indexmethod + 1);
                          indexmethod=row.IndexOf('"');
                          Titles.Add(row.Substring(0, indexmethod));
                          row = row.Substring(indexmethod +2);
                          Genres.Add(row.Replace("|",","));
                    }
                    
                 }
                 sr.Close();
        }
        catch (Exception a)
        {logger.Error(a, "Error formatting data");}
        string choice;
            do
            {
                Console.WriteLine("Welcome to the movie library!");
                Console.WriteLine("Press 1 to Add Movies");
                Console.WriteLine("Press 2 to List Movies");
                Console.WriteLine("Enter any other key to exit.");
                choice = Console.ReadLine();

               if (choice == "1")
                    {
                    
                        Console.WriteLine("Enter the movie title");
                        string userMovie = Console.ReadLine();
                        List<string> LowerCaseMovieTitles = Titles.ConvertAll(t => t.ToLower());
                        if (LowerCaseMovieTitles.Contains(userMovie.ToLower()))
                        {
                            logger.Info("Duplicate movie", Titles);
                        }
                        else
                        {
                            UInt64 movieId = Ids.Max() + 1;
                            List<string> movieGenres = new List<string>();
                            string genre;
                            do
                            {
                                Console.WriteLine("Enter genre (or 'quit' to quit)");
                                genre = Console.ReadLine();
                                if (genre != "quit" && genre.Length > 0)
                                {
                                    movieGenres.Add(genre);
                                }
                            } while (genre != "quit");
                            if (Genres.Count == 0)
                            {
                                movieGenres.Add("(no genres listed)");
                            }
                            string category = string.Join("|", movieGenres);
                            userMovie = userMovie.IndexOf(',') != -1 ? $"\"{userMovie}\"" : userMovie;
                            StreamWriter sw = new StreamWriter(file, true);
                            sw.WriteLine($"{movieId},{userMovie},{category}");
                            sw.Close();
                            Ids.Add(movieId);
                            Titles.Add(userMovie);
                            Genres.Add(category);
                      }
                    }
                
                else if (choice == "2")
                {
                    StreamReader sr = new StreamReader(file);
                    while(!sr.EndOfStream)
                    {
                       for (int q = 0; q < Ids.Count; q++)
                        {
                            Console.WriteLine($"Id: {Ids[q]}");
                            Console.WriteLine($"Title: {Titles[q]}");
                            Console.WriteLine($"Genres: {Genres[q]}");
                            Console.WriteLine();
                        }
                    }
                 
                       
                        
                }
               
             } while (choice == "1" || choice == "2");
            logger.Info("Program Ended");
        
         }
    }
}
}
    


