using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppQuoteFinder.DataAccess
{
    public interface IQuotesApiDataReader
    {
        public Task<string> ReadAsync(int page, int quotesPerPage);
    }
}
