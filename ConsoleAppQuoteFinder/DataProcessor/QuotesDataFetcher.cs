using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppQuoteFinder.DataAccess;

namespace ConsoleAppQuoteFinder.DataProcessor
{
    public class QuotesDataFetcher
    {
        private readonly IQuotesApiDataReader _quotesApiDataReader;

        public QuotesDataFetcher(IQuotesApiDataReader quotesApiDataReader)
        {
            _quotesApiDataReader = quotesApiDataReader;
        }

        public async Task<IEnumerable<string>> FetchDataFromPagesAsync(int page, int quoteAmount)
        {
            var tasks = new List<Task<string>>();

            for (int i = 0; page > i; i++)
            {
                var fetchedTask = _quotesApiDataReader.ReadAsync(i + 1, quoteAmount);
                tasks.Add(fetchedTask);
            }

            return await Task.WhenAll(tasks);
        }
    }
}
