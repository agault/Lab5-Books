using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5_Books.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string BookTitle { get; set; }
       /* public string AuthorTitle { get; set; }*/
        public string AuthorFName { get; set; }
        public string AuthorLName { get; set; }
       
    }
}
