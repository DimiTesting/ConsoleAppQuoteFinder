using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConsoleAppQuoteFinder.DataProcessor.Interfaces;
using ConsoleAppQuoteFinder.Models;

namespace ConsoleAppQuoteFinder.DataProcessor
{
    public class QuotesToStringConverter : IQuotesToStringConverter
    {
        public string Convert(string json)
        {
            var quotes = DeserializeJsonToString(json);
            var result = string.Join(Environment.NewLine, quotes);
            return result;
        }

        private List<string> DeserializeJsonToString(string json)
        {
            List<string> quotes = [];

            Root root = JsonSerializer.Deserialize<Root>(json);
            List<Datum> data = root.data;

            foreach (Datum datum in data)
            {
                quotes.Add(datum.quoteText);
            }

            return quotes;
        }
    }
}
