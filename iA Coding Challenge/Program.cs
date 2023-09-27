//  ***  HOW WOULD I CHANGE THIS PROGRAM TO SUPPORT MULTIPLE CENTRAL FILLS AT THE SAME LOCATION?  ***
//  1. Instead of using a List<CentralFillFacility, I would probably use Dictionary<Tuple<int, int>, List <CentralFillFacility>> where the 
//     key is a tuple representing the X & Y coordinates, and the value is a list containing all facilities at that location.
//  2. When calculating the distance to a location with multiple facilities, I would probably calculate the distance once per location instead of per facility.
//  3. When displaying the facilities, I would have to decide whether to display all facilities at the same location or still list them separately.
//  4. Handle edges cases where multiple facilities are at the same location.
//  5. I would probably have to change the way the user interacts with the program to handle multiple facilities at the same location.
//  6. Add lots of testing around multiple facilities at same location in order to cover any scenario that may happen.

//  *** HOW WOULD I CHANGE THE PROGRAM IF IT WAS A MUCH LARGER WORLD SIZE? ***
//  1. I would optimize the storage and lookup to handle more facilities. Possibly a quadrant methodology could work here to prevent from having to look through all of the facilities
//  2. I would use a database for memory management. Adding a much larger world would be hard to manage with just code and memory.
//  3. I would enhance the user interface to make it easier for users to input coordinates and maybe include a map or search feature.

using System.Linq;
using System;
using System.Collections.Generic;

namespace iA_Coding_Challenge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Generating a list of Central Fill facilities with random coordinates and medication prices
            var centralFills = GenerateCentralFills();

            // Prompt the user to input their coordinates
            Console.WriteLine("Please Input Coordinates:");

            // Read the user input and split it by ',' to get the X and Y coordinates
            // I am assuming the user will give me a correct input each time, such as '5,4'
            // There is potential here for an edge case. If the user does not enter data in the correct format, the application will crash
            // There could be lots of input validation code here
            var input = Console.ReadLine()?.Split(',');

            // Making sure the user inputted something, but really needs more validation code
            if (input != null)
            {
                // Parsing the X and Y coordinates to integers
                int x = int.Parse(input[0]);
                int y = int.Parse(input[1]);

                // Process the list of Central Fill facilities to find the three closest to the user’s input coordinates
                var closestFills = centralFills.Select(cf => new
                {
                    // Store the reference to the Central Fill object
                    CentralFill = cf,

                    // Calculate the Manhattan Distance between the Central Fill and the user input coordinates
                    // Absolute value is used here to turn any possible negative numbers to positive, as negative distances do not exist in the real world
                    Distance = Math.Abs(x - cf.X) + Math.Abs(y - cf.Y),

                    // Find the medication with the lowest price in the Central Fill
                    CheapestMedication = cf.Medications.Aggregate((l, r) => l.Value < r.Value ? l : r)
                })
                    // Ordering the Central Fill facilities first by Distance, then by the price of the cheapest medication
                    .OrderBy(cf => cf.Distance)
                    .ThenBy(cf => cf.CheapestMedication.Value)
                    // Take the first three Central Fill facilities from the ordered list
                    .Take(3);

                // Output the details of the three closest Central Fill facilities
                Console.WriteLine($"Closest Central Fills to ({x},{y}):");
                foreach (var cf in closestFills)
                {
                    // Format and print the details of each Central Fill facility
                    Console.WriteLine($"Central Fill {cf.CentralFill.Id} - ${cf.CheapestMedication.Value:F2}, Medication {cf.CheapestMedication.Key}, Distance {cf.Distance}");

                    // Uncommenting the below line will show the coordinates of the facility in order to verify their distance
                    // Console.WriteLine($"With coordinates of X: {cf.CentralFillFacility.X}, Y: {cf.CentralFillFacility.Y}");
                }
            }
            // Pausing the program so we can see the result of the Console.Writeline()
            Console.ReadLine();
        }

        // Method to generate a list of Central Fill facilities with random coordinates and medication prices
        static List<CentralFillFacility> GenerateCentralFills()
        {
            var random = new Random(); // Initialize a new random number generator
            var centralFills = new List<CentralFillFacility>(); // Initialize a new list to hold the Central Fill facilities

            // I'm assuming we want to generate 10 Central Fill facilities
            for (int i = 0; i < 10; i++)
            {
                // Initialize a new dictionary to hold the names and prices of medications
                var medications = new Dictionary<string, decimal>
                {
                    // Generating random prices for Medications A, B, and C
                    // Converting to decimal and dividing by 10.0 to mimic a USD price
                    { "A", (decimal)(random.Next(1, 1000) / 10.0) },
                    { "B", (decimal)(random.Next(1, 1000) / 10.0) },
                    { "C", (decimal)(random.Next(1, 1000) / 10.0) }
                };

                // Creating a new Central Fill object with random coordinates, unique Id, and the generated medications
                centralFills.Add(new CentralFillFacility
                {
                    Id = (i + 1).ToString("D3"), // Format the Id as a three-digit string
                    X = random.Next(-10, 11), // Generate a random X coordinate between -10 and 10
                    Y = random.Next(-10, 11), // Generate a random Y coordinate between -10 and 10
                    Medications = medications // Assign the generated medications to the Central Fill
                });
            }

            // Returning the list of generated Central Fill facilities
            return centralFills;
        }
    }
}

