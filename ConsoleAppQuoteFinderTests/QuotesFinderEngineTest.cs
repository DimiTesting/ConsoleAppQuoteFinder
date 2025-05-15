using ConsoleAppQuoteFinder.DataProcessor;
using NUnit.Framework;

namespace ConsoleAppQuoteFinderTests
{
    public class QuotesFinderEngineTest
    {
        [TestCase("Catch me outside how about that", "that", "Catch me outside how about that")]
        [TestCase("Wiggle wiggle you got to see the eagle", "got", "Wiggle wiggle you got to see the eagle")]
        [TestCase("It is my liiifeeee", "me", "No quotes found")]
        public void DisplayQuoteIfWordIsInIt(string quotes, string word, string expected)
        {
            var quotesFinderEngine = new QuoteFinderEngine();

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                quotesFinderEngine.DisplayFoundQuotes(quotes, word);

                var result = sw.ToString().Trim();

                Assert.AreEqual(result, expected);
            }
        }
    }
}
