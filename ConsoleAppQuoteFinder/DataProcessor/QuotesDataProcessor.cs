using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppQuoteFinder.DataProcessor.Interfaces;

namespace ConsoleAppQuoteFinder.DataProcessor
{
    public class QuotesDataProcessor
    {
        private readonly IQuotesToStringConverter _quotesToStringConverter;
        private readonly IQuoteFinderEngine _quoteFinderEngine;

        public QuotesDataProcessor(IQuotesToStringConverter quotesToStringConverter, IQuoteFinderEngine quoteFinderEngine)
        {
            _quotesToStringConverter = quotesToStringConverter;
            _quoteFinderEngine = quoteFinderEngine;
        }
        public async Task ProcessAsync(IEnumerable<string> data, string word, string processParallel)
        {
            if (processParallel == "y")
            {

                var tasks = data.Select(page => Task.Run(() => {
                    var convertedData = _quotesToStringConverter.Convert(page);
                    _quoteFinderEngine.DisplayFoundQuotes(convertedData, word);
                }));

                await Task.WhenAll(tasks);
            }
            else
            {
                foreach (var page in data)
                {
                    var convertedData = _quotesToStringConverter.Convert(page);
                    _quoteFinderEngine.DisplayFoundQuotes(convertedData, word);
                }

            }
        }
    }

}
