using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppQuoteFinder.DataProcessor.Interfaces
{
    public interface IQuoteFinderEngine
    {
        public void DisplayFoundQuotes(string quotes, string userInput);
    }
}
