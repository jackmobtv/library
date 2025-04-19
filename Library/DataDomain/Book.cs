using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataDomain
{
    public class Book
    {
        public int BookID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int GenreId { get; set; }
        public int PublisherId { get; set; }
        public int AuthorId { get; set; }
    }
}
