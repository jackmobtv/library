using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;

namespace DataAccessFakes
{
    public class BookAccessorFakes : IBookAccessor
    {
        private List<Book> _books;

        public BookAccessorFakes() 
        { 
            _books = new List<Book>();

            _books.Add(new Book()
            {
                BookId = 100000,
                Name = "Tubas and Company",
                Description = "Holiday Tubas",
                Genre = "Horror",
                Author = "Joe Mama",
                Publisher = "Ligma Balls",
            });

            _books.Add(new Book()
            {
                BookId = 100001,
                Name = "Macrohard",
                Description = "The Story of Windows ME",
                Genre = "Biography",
                Author = "John Microsoft",
                Publisher = "John Apple",
            });

            _books.Add(new Book()
            {
                BookId = 100002,
                Name = "Die Hard: The Movie The Novel The Movie The Novel",
                Description = "Dying Hard or Hardly Dying",
                Genre = "Non-Fiction",
                Author = "John McClaine",
                Publisher = "Hans Gruber",
            });
        }

        public List<Book> selectAllBooks()
        {
            return _books;
        }

        public Book selectBookById(int bookId)
        {
            foreach (var book in _books)
            {
                if(book.BookId == bookId)
                {
                    return book;
                }
            }
            throw new ArgumentException("Book Not Found");
        }
    }
}
