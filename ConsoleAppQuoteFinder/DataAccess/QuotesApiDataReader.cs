using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppQuoteFinder.DataAccess.Mock;

namespace ConsoleAppQuoteFinder.DataAccess
{
    public class QuotesApiDataReader : IQuotesApiDataReader
    {
        private readonly MockQuotesApiDataReader _mockQuotesApiDataReader;

        public QuotesApiDataReader(MockQuotesApiDataReader mockQuotesApiDataReader)
        {
            _mockQuotesApiDataReader = mockQuotesApiDataReader;
        }
        public async Task<string> ReadAsync(int pages, int quantity)
        {
            try
            {
                string quotes = await _mockQuotesApiDataReader.ReadAsync(pages, quantity);
                return quotes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Data could not be fetched from endpoint - {ex.Message}.");
                throw;
            }
        }
    }
}
