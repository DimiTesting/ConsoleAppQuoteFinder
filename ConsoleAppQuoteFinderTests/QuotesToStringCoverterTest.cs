using NUnit.Framework;
using ConsoleAppQuoteFinder.DataProcessor;

namespace ConsoleAppQuoteFinderTests
{
    public class QuotesToStringCoverterTest
    {
        [Test]
        public void ConvertShallReturnMultilineStringFromJsonContainingKeysDataAndQuoteText()
        {
			var converter = new QuotesToStringConverter();

			var input = @"{
			  ""statusCode"": 200, 
			  ""message"": ""success"", 
			  ""pagination"" :  {
						""page"": 1
			  },
			  ""totalQuotes"": 10, 
			  ""data"" : 
				[
				 {
				   ""_id"" : ""1"", 
				   ""quoteText"": ""Mock1 text 1"",
				   ""quoteAuthor"": ""Mock"", 
				   ""quoteGenre"": ""Mock"", 
				   ""_v"": 1
				 },
				 {
				   ""_id"" : ""2"", 
				   ""quoteText"": ""Mock2 text 2"",
				   ""quoteAuthor"": ""Mock"", 
				   ""quoteGenre"": ""Mock"", 
				   ""_v"": 2
				 }
				]
			}";

            var result = converter.Convert(input);

            var expected = $"Mock1 text 1{Environment.NewLine}Mock2 text 2";

            Assert.AreEqual(expected, result, "Json file should be formated as defined in Models folder");
        }

		[Test]
		public void ConvertShallThrowExceptionFromJsonNotContainingKeysDataAndQuoteText()
		{
            var converter = new QuotesToStringConverter();

			var input = "Not correctly formatted";

			Assert.Throws<System.Text.Json.JsonException>(
				() => converter.Convert(input), "Json file should be formated as defined in Models folder");
        }
    }
}
