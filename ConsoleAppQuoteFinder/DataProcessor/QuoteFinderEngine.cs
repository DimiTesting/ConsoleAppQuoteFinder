using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppQuoteFinder.DataProcessor.Interfaces;

namespace ConsoleAppQuoteFinder.DataProcessor
{
    public class QuoteFinderEngine : IQuoteFinderEngine
    {
        public void DisplayFoundQuotes(string quotes, string userInput)
        {
            var foundQuotes = Find(quotes, userInput);

            if (foundQuotes.Count == 0)
            {
                Console.WriteLine("No quotes found");
            }

            for (int i = 0; i < foundQuotes.Count; i++)
            {
                Console.WriteLine(foundQuotes[i]);
            }

        }
        private List<string> Find(string quotes, string userInput)
        {
            var result = new List<string>();

            string[] eachQuote = quotes.Split('\n');

            for (int i = 0; i < eachQuote.Length; i++)
            {
                var splittedQuotes = eachQuote[i].Split(' ');

                for (int j = 0; j < splittedQuotes.Length; j++)
                {
                    if (splittedQuotes[j] == userInput)
                    {
                        result.Add(string.Join(" ", splittedQuotes));
                    }
                }
            }
            return result;
        }
    }
}
