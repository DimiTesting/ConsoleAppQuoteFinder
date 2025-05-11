using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppQuoteFinder.Models
{
    public class Root
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public Pagination pagination { get; set; }
        public int totalQuotes { get; set; }
        public List<Datum> data { get; set; }
    }
}
