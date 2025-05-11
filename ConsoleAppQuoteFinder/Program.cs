using System.Text.Json;
using ConsoleAppQuoteFinder.DataAccess;
using ConsoleAppQuoteFinder.DataAccess.Mock;
using ConsoleAppQuoteFinder.Models;

namespace ConsoleAppQuoteFinder
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var mockAPI = new MockQuotesApiDataReader();

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

            var quotesDataProcessor = new QuotesDataProcessor();

            await quotesDataProcessor.ProcessAsync(fetchedQuotes, 
                searchingWord, 
                chosenOption);
        }
    }

    public class QuotesApiDataReader: IQuotesApiDataReader
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

            for(int i=0; page>i; i++)
            {
                var fetchedTask = _quotesApiDataReader.ReadAsync(i+1, quoteAmount);
                tasks.Add(fetchedTask);
            }

            return await Task.WhenAll(tasks);
        }
    }

    public class QuotesDataProcessor
    {
        public async Task ProcessAsync(IEnumerable<string> data, string word, string processParallel)
        {
            if(processParallel == "y")
            {
                var tasks = data.Select(page => Task.Run(() => ProcessPage(page, word)));

                await Task.WhenAll(tasks);
            }
            else
            {
                foreach (var page in data)
                {
                    ProcessPage(page, word);
                }

            }
        }

        private void ProcessPage(string pageJson, string word)
        {
            var root = JsonSerializer.Deserialize<Root>(pageJson);

            var quoteWithWord = root?.data
                .Where(quote => quote.quoteText.Contains(word))
                .MinBy(quote => quote.quoteText.Length);

            if (quoteWithWord is not null)
            {
                Console.WriteLine(
                    $"{quoteWithWord.quoteText} -- {quoteWithWord.quoteAuthor}");
            }
            else
            {
                Console.WriteLine("No quote found on this page.");
            }
        }
    }
    
    public class QuotesToStringConverter
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

    public class QuoteFinderEngine
    {
        public void DisplayFoundQuotes(string quotes, string userInput)
        {
            var foundQuotes = Find(quotes, userInput);

            if(foundQuotes.Count == 0)
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

                for(int j = 0; j < splittedQuotes.Length; j++)
                {
                    if(splittedQuotes[j] == userInput)
                    {
                        result.Add(string.Join(" ", splittedQuotes));
                    }
                }
            }
            return result;
        }
    }

    public class Communicator : IUserInteractor
    {
        public string ReadSingleWord(string question)
        {
            string validWord;
            do
            {
                Console.WriteLine(question);
                validWord = Console.ReadLine();
            }
            while(!ValidateString(validWord));
            return validWord;
        }

        private bool ValidateString(string word)
        {
            return word!=null && word.All(char.IsLetter);
        }

        public int IsNumberValid(string input)
        {
            int number;
            do
            {
                Console.WriteLine(input);
            }
            while (!int.TryParse(Console.ReadLine(), out number));
            return number;
        }

        public string ValidateChosenOption(string message)
        {
            Console.WriteLine(message);
            var input = Console.ReadLine();
            if(input == "y")
            {
                return "y";
            }
            return "false";
        }

    }

    public interface IUserInteractor
    {
        string ReadSingleWord(string input);
        int IsNumberValid(string input);
        string ValidateChosenOption(string input);
    }
}