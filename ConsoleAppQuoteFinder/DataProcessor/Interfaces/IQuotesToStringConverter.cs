using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppQuoteFinder.DataProcessor.Interfaces
{
    public interface IQuotesToStringConverter
    {
        public string Convert(string json);
    }
}
