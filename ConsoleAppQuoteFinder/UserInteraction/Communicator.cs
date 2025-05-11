using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppQuoteFinder.UserInteraction.Interfaces;

namespace ConsoleAppQuoteFinder.UserInteraction
{
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
            while (!ValidateString(validWord));
            return validWord;
        }

        private bool ValidateString(string word)
        {
            return word != null && word.All(char.IsLetter);
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
            if (input == "y")
            {
                return "y";
            }
            return "false";
        }

    }
}
