using System;
using System.IO;

namespace HourCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read the current total minutes from the file
            int totalMinutes = ReadTotalMinutesFromFile();

            while (true)
            {
                // Display the options to the user
                Console.WriteLine("1. Add hours and minutes");
                Console.WriteLine("2. View total time");
                Console.WriteLine("3. Exit");
                Console.WriteLine("Enter your choice: ");

                // Get the user's choice
                int choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    // Get the number of hours and minutes to add from the user
                    Console.WriteLine("Enter the number of hours to add: ");
                    int hoursToAdd = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter the number of minutes to add: ");
                    int minutesToAdd = int.Parse(Console.ReadLine());

                    // Convert the hours to minutes and add them to the total
                    totalMinutes += hoursToAdd * 60 + minutesToAdd;

                    // Write the new total minutes to the file
                    WriteTotalMinutesToFile(totalMinutes);

                    Console.WriteLine("Hours and minutes added successfully!");
                }
                else if (choice == 2)
                {
                    // Calculate the total hours and minutes
                    int totalHours = totalMinutes / 60;
                    int remainingMinutes = totalMinutes % 60;

                    // Display the total time to the user
                    Console.WriteLine("\r\nTotal hours: " + totalHours);
                    Console.WriteLine("Total minutes: " + remainingMinutes + "\r\n");
                }
                else if (choice == 3)
                {
                    // Exit the program
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
        }

        static int ReadTotalMinutesFromFile()
        {
            // Check if the file exists
            if (!File.Exists("total_minutes.txt"))
            {
                // If the file doesn't exist, create it and return 0
                File.Create("total_minutes.txt").Dispose();
                return 0;
            }

            // Read the contents of the file and parse it as an integer
            string contents = File.ReadAllText("total_minutes.txt");
            return int.Parse(contents);
        }

        static void WriteTotalMinutesToFile(int totalMinutes)
        {
            // Write the total minutes to the file as a string
            File.WriteAllText("total_minutes.txt", totalMinutes.ToString());
        }
    }
}
