using System.Text.Json;
using ConsoleAppQuoteFinder.DataAccess;
using ConsoleAppQuoteFinder.DataAccess.Mock;
using ConsoleAppQuoteFinder.DataProcessor;
using ConsoleAppQuoteFinder.UserInteraction;

namespace ConsoleAppQuoteFinder
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var mockAPI = new MockQuotesApiDataReader();
            var quotesToStringConverter = new QuotesToStringConverter();
            var quoteFinderEngine = new QuoteFinderEngine();

            var communicator = new Communicator();

            string searchingWord = communicator.ReadSingleWord("Insert a word which you are looking for");
            int numberOfPages = communicator.IsNumberValid("How many pages do you want to read?");
            int numberOfQuotes = communicator.IsNumberValid("How many quotes per page?");
            string chosenOption = communicator.ValidateChosenOption("Shall we process the page in parallel? " +
                "('y' is yes and everything else is no");

            var quotesApiDataReader = new QuotesApiDataReader(mockAPI);
            var quotesDataFetcher = new QuotesDataFetcher(quotesApiDataReader);

            var fetchedQuotes = await quotesDataFetcher.FetchDataFromPagesAsync(
                numberOfPages, 
                numberOfQuotes);

            var quotesDataProcessor = new QuotesDataProcessor(quotesToStringConverter, quoteFinderEngine);

            await quotesDataProcessor.ProcessAsync(fetchedQuotes, 
                searchingWord, 
                chosenOption);
        }
    }
}