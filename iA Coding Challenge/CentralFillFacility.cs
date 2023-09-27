using System.Collections.Generic;

namespace iA_Coding_Challenge
{
    public class CentralFillFacility // Class representing a unique Central Fill facility
    {
        public string Id { get; set; } // Unique Identifier for each facility
        public int X { get; set; } // X Coordinate in the world
        public int Y { get; set; } // Y Coordinate in the world
        public Dictionary<string, decimal> Medications { get; set; } // Dictionary to hold Medication names as keys and their prices as values

        // If this was a real world project, I am sure there would be much more code here to capture more information about the Central Fill facility, etc.
    }
}