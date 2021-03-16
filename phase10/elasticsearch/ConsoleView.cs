using System;
using System.Collections.Generic;
using System.Linq;

namespace elasticsearch
{
    public class ConsoleView : IView
    {
        public string GetUserInput()
        {
            Console.WriteLine("Enter the sentence to Search!");
            return Console.ReadLine();
        }

        public void ShowSearchResult(HashSet<string> searchResult)
        {
            if (!searchResult.Any())
            {
                Console.WriteLine("No Match Was Found!");
                return;
            }

            Console.WriteLine($"Total number of match is {searchResult.Count}");
            foreach (var result in searchResult)
            {
                Console.WriteLine(result);
            }
        }
    }
}