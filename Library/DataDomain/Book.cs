using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DataDomain
{
    public class Book
    {
        public int BookID { get; set; }

        [Required]
        [DisplayName("Book Title")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [DisplayName("Book Description")]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string Genre { get; set; }

        [Required]
        [MaxLength(255)]
        public string Author { get; set; }

        [Required]
        [MaxLength(255)]
        public string Publisher { get; set; }

        public int GenreId { get; set; }

        public int PublisherId { get; set; }

        public int AuthorId { get; set; }
    }
}
