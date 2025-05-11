using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppQuoteFinder.Models
{
    public class Pagination
    {
        public int currentPage { get; set; }
        public int nextPage { get; set; }
        public int totalPages { get; set; }
    }
}
