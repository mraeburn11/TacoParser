﻿using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;
using System.ComponentModel.DataAnnotations;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // TODO:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You'll need two nested forloops ---------------------------

            logger.LogInfo("Log initialized");

            // DONE use File.ReadAllLines(path) to grab all the lines from your csv file
            // DONE Log and error if you get 0 lines and a warning if you get 1 line
            var lines = File.ReadAllLines(csvPath);

            if(lines.Length == 0)
            {
                logger.LogError($"File doesn't contain anything!!");
            }

            if(lines.Length == 1)
            {
                logger.LogWarning($"File only contains one line");
            }
            
            
            logger.LogInfo($"Lines: {lines[0]}");



            // DONE  Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
            var locations = lines.Select(parser.Parse).ToArray();

            // DON'T FORGET TO LOG YOUR STEPS

            // Now that your Parse method is completed, START BELOW ----------

            // DONE TODO: Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the farthest from each other.
            // DONE Create a `double` variable to store the distance

            ITrackable Tacobell1 = null;
            ITrackable Tacobell2 = null;

            var largestDistance = 0.0;


            // DONE Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`

            //HINT NESTED LOOPS SECTION---------------------
            // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)

            foreach (var location in locations)
            {
                var locA = location;
                var corA = new GeoCoordinate();
                corA.Latitude = locA.Location.Latitude;
                corA.Longitude = locA.Location.Longitude;

                foreach(var location1 in locations)
                {
                    var locB = location1;
                    var corB = new GeoCoordinate();
                    corB.Latitude = locB.Location.Latitude;
                    corB.Longitude = locB.Location.Longitude;

                    var distanceBetween = corA.GetDistanceTo(corB);

                if (distanceBetween > largestDistance)
                    {
                        largestDistance = distanceBetween;
                        Tacobell1 = locA;
                        Tacobell2 = locB;
                    }
                }
            }

            // DONE Create a new corA Coordinate with your locA's lat and long

            // DONE Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)

            // DONE Create a new Coordinate with your locB's lat and long

            // DONE Now, compare the two using `.GetDistanceTo()`, which returns a double
            // If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above

            // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.

            Console.WriteLine($"{Tacobell1.Name} and {Tacobell2.Name} are the farthest apart from each other!");


            
        }
    }
}
