using System;
using NLog.Web;
using System.IO;

namespace MovieLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            string choice;
            string file = "C:\\Users\\Ethan Computer\\Documents\\DotNet\\MovieLibrary\\ml-latest-small\\movies.csv";
            
            
            string path = Directory.GetCurrentDirectory() + "\\nlog.config";
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();
            logger.Info("Program Started");
            
            Console.WriteLine("Tets");

            do
            {
           
                Console.WriteLine("Welcome to the movie library!");
                Console.WriteLine("Press 1 to List Movies");
                Console.WriteLine("Press 2 to Add Movies");
                Console.WriteLine("Enter any other key to exit.");
                choice = Console.ReadLine();

                if (choice == "1")
                {
                    
                  if (!File.Exists(file))
                  {
                      logger.Error("File doesn't exist: {File}", file);
                      Console.ReadLine();
                  }
                  else
                    {
                        
                        
                        StreamReader sr = new StreamReader(file);
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            string[] film= line.Split(',');
                            string movieID= film[0];
                            string title=film[1];
                            string genre=film[2];



                            Console.WriteLine($"MovieID: {movieID}");
                            Console.WriteLine($"Title: {title}");
                            Console.WriteLine($"Genre: {genre}");
                            Console.WriteLine("__________________________");
                            
                        }
                        sr.Close();
                    }
                  
                }
                else if (choice=="2")
                {
                    
                    Console.WriteLine("Movie Title:");
                    String UserMovie = Console.ReadLine();
                    
                }
               
            } while (choice == "1" || choice == "2");
            logger.Info("Program Ended");
        }
    }
}

