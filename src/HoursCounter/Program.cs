using System;
using System.IO;
using System.Collections.Generic;

namespace TimeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read the current total minutes from the file
            int totalMinutes = ReadTotalMinutesFromFile();

            // Read the list of sessions from the file
            List<string> sessions = ReadSessionsFromFile();

            string currentSession = "";

            while (true)
            {
                // Display the options to the user
                Console.WriteLine("1. Add/update session");
                Console.WriteLine("2. View total time");
                Console.WriteLine("3. View sessions list");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Enter your choice: ");

                // Get the user's choice
                int choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    if (currentSession == "")
                    {
                        // Get the start time from the user
                        Console.WriteLine("Enter the start time (HH:MM am/pm DDMMYYYY): ");
                        string startTime = Console.ReadLine();

                        // Check if the start time is correct
                        if (IsValidTime(startTime))
                        {
                            currentSession = startTime;
                            Console.WriteLine("Start time saved successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid start time. Please try again.");
                        }
                    }
                    else
                    {
                        // Get the end time from the user
                        Console.WriteLine("Enter the end time (HH:MM am/pm DDMMYYYY): ");
                        string endTime = Console.ReadLine();

                        // Check if the end time is correct
                        if (IsValidTime(endTime))
                        {
                            // Update
currentSession += " - " + endTime;
                            sessions.Add(currentSession);
                            WriteSessionsToFile(sessions);

                            // Calculate the duration of the session
                            int startMinutes = ParseTime(currentSession.Substring(0, 20));
                            int endMinutes = ParseTime(endTime);
                            int sessionMinutes = endMinutes - startMinutes;

                            // Add the duration of the session to the total minutes
                            totalMinutes += sessionMinutes;
                            WriteTotalMinutesToFile(totalMinutes);

                            currentSession = "";
                            Console.WriteLine("Session added successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid end time. Please try again.");
                        }
                    }
                }
                else if (choice == 2)
                {
                    // Calculate the total hours and minutes
                    int totalHours = totalMinutes / 60;
                    int remainingMinutes = totalMinutes % 60;

                    // Display the total time to the user
                    Console.WriteLine("Total hours: " + totalHours);
                    Console.WriteLine("Total minutes: " + remainingMinutes);
                }
                else if (choice == 3)
                {
                    // Display the list of sessions to the user
                    foreach (string session in sessions)
                    {
                        Console.WriteLine(session);
                    }
                }
                else if (choice == 4)
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

        static List<string> ReadSessionsFromFile()
        {
            // Check if the file exists
            if (!File.Exists("sessions.txt"))
            {
                // If the file doesn't exist, create it and return an empty list
                File.Create("sessions.txt").Dispose();
                return new List<string>();
            }

            // Read the contents of the file and return a list of sessions
            string contents = File.ReadAllText("sessions.txt");
            return new List<string>(contents.Split('\n'));
        }

        static void WriteSessionsToFile(List<string> sessions)
        {
            // Write the list of sessions to
            // the file as a string
            File.WriteAllText("sessions.txt", string.Join('\n', sessions));
        }

        static bool IsValidTime(string time)
        {
            // Check if the time is 20 characters long
            if (time.Length != 20)
            {
                return false;
            }

            // Check if the time has the correct format
            if (!time.Substring(2, 1).Equals(":") || !time.Substring(8, 1).Equals(" "))
            {
                return false;
            }

            // Check if the hours and minutes are valid
            if (!int.TryParse(time.Substring(0, 2), out int hours) || hours < 0 || hours > 12 ||
                !int.TryParse(time.Substring(3, 2), out int minutes) || minutes < 0 || minutes > 59)
            {
                return false;
            }

            // Check if the AM/PM indicator is valid
            string ampm = time.Substring(6, 2);
            if (!ampm.Equals("am") || !ampm.Equals("pm"))
            {
                return false;
            }

            // Check if the date is valid
            if (!int.TryParse(time.Substring(9, 2), out int day) || day < 1 || day > 31 ||
                !int.TryParse(time.Substring(16, 4), out int year) || year < 2023)
            {
                return false;
            }

            // If all checks pass, the time is valid
            return true;
        }

        static int ParseTime(string time)
        {
            // Parse the hours and minutes from the time string
            int hours = int.Parse(time.Substring(0, 2));
            int minutes = int.Parse(time.Substring(3, 2));

            // Check if the time is PM and add 12 hours if it is
            if (time.Substring(6, 2).Equals("pm"))
            {
                hours += 12;
            }

            // Return the total number of minutes
            return hours * 60 + minutes;
        }
    }
}
