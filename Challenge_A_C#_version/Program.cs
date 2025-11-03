using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_A_C__version
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool running = true;
            //loop to keep the program running until the user decides to exit
            while (running != false)
            {
                Console.Clear();
                Menu();
                
                int choice = GetValidIntegerInput(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        CalculateSteps();

                        Console.WriteLine("Press any key to return to the main menu...");
                        Console.ReadLine();
                        break;
                    case 2:
                        //Exit the program
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Option, Please try again");
                        Console.WriteLine("Press any key to try again");
                        Console.ReadKey();
                        break;
                }
            }
            Console.ReadLine();
        }

        //Menu method to display options to the user
        public static void Menu()
        {
            Console.WriteLine("Welcome to the bay change Assistant, Select an Option");
            Console.WriteLine("1. Calculate How much steps remain");
            Console.WriteLine("2. Exit");
            Console.Write("Enter your choice: ");
        }

        // Validation to check if the input is a valid integer
        public static int GetValidIntegerInput(string input)
        {
            int number;

            //Try to convert the string to a number
            if (int.TryParse(input, out number))
            {
                //It was a number. Now, check if it's negative
                if (number < 0)
                {
                    // It's a number, but it's negative
                    return -1; // Return a "failure" value
                }
                else
                {
                    // It's a valid, non-negative number, return the number as an integer
                    return number;
                }
            }
            else
            {
                // It was a letter
                
                Console.WriteLine("Error: That is not a valid number. Please enter an integer.");
                
                return -1; // Return a failure value
            }

        }

        //Handle the calculation of steps
        public static void CalculateSteps()
        {
            int totalSteps;
            int completedSteps;
            string input;

            // Loop to get validated Total Steps 
            while (true)
            {
                Console.Write("Enter the total number of steps: ");
                input = Console.ReadLine();

                //Validate - returns -1 if invalid, the number if valid
                totalSteps = GetValidIntegerInput(input);

                // Check if valid number was entered
                if (totalSteps != -1)
                {
                    break; // Valid number, exit the loop
                }
                
            }

            //Loop to get validated Completed Steps ---
            while (true)
            {
                Console.Write("Enter the number of steps already completed: ");
                input = Console.ReadLine();

                //Validate - returns -1 if invalid, the number if valid
                completedSteps = GetValidIntegerInput(input);

                //Check for all error conditions
                if (completedSteps == -1)
                {
                    // Input was not a valid number. Loop again.
                }
                else if (completedSteps > totalSteps)
                {
                    
                    Console.WriteLine($"\nError: Completed steps ({completedSteps}) cannot be greater than total steps ({totalSteps}).");
                    
                    // Loop again
                }
                else
                {
                    // Valid number and valid logic. Exit the loop.
                    break;
                }
            }

            //Get Bay Name
            
            Console.Write("Enter the current bay name (e.g., \"Bay 3\"): ");
            
            string bayName = Console.ReadLine();

            //Calculate and Display
            if (completedSteps == totalSteps)
            {
                Console.WriteLine("\n--- Calculation Complete ---");
                Console.WriteLine("Machine completed assembly — ready for next bay?");
            }
            else
            {
                int remainingSteps = totalSteps - completedSteps;
                
                double progressPercent = (double)completedSteps / totalSteps;

                Console.WriteLine($"Machine currently in {bayName}");
                Console.WriteLine($"Steps completed: {completedSteps} of {totalSteps}");
                Console.WriteLine($"Remaining steps: {remainingSteps}");
                // "P0" formats as a percentage
                Console.WriteLine($"Progress: {progressPercent:P0}");
            }
        }
    }
}
