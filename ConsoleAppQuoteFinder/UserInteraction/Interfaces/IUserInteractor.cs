using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppQuoteFinder.UserInteraction.Interfaces
{
    public interface IUserInteractor
    {
        string ReadSingleWord(string input);
        int IsNumberValid(string input);
        string ValidateChosenOption(string input);
    }
}
