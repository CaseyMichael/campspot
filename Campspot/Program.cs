using System;
using System.IO;
using Campspot.Repositories;

namespace Campspot
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = @"..\..\..\test-case.json";
            if (args.Length == 0)
            {
                Console.WriteLine("No file supplied, using default \"test-case.json\" file.");
            }
            if (args.Length > 0)
            {
                if (File.Exists(args[0]))
                {
                    Console.WriteLine($"Using the specified \"{args[0]}\" file.");
                    filePath = args[0];
                }
                else
                {
                    Console.WriteLine($"Could not find the specified \"{args[0]}\" file, using default \"test-case.json\" file.");
                }
            }

            ImportTestCases importTestCases = new ImportTestCases(filePath);
            SearchQueryRepository searchQueryRepository = new SearchQueryRepository(importTestCases);
            ReservationEngine engine = ReservationEngineFactory.Create(filePath);
            var availableCampsites = engine.GetAvailableCampsitesForSearchQuery(searchQueryRepository.GetSearchQuery());
            foreach (var campsite in availableCampsites)
            {
                Console.WriteLine($"{campsite.Id} - {campsite.Name}");
            }
            Console.ReadKey();
        }
    }
}
