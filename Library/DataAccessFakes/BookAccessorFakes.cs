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
        private List<Copy> _copies;

        public BookAccessorFakes() 
        { 
            _books = new List<Book>();
            _copies = new List<Copy>();

            _books.Add(new Book()
            {
                BookId = 100000,
                Name = "Tubas and Company",
                Description = "Holiday Tubas",
                Genre = "Horror",
                Author = "Joe Mama",
                Publisher = "Ligma Balls",
            });

            _copies.Add(new Copy()
            {
                CopyId = 100000,
                BookId = 100000,
                Condition = "Good",
                Active = true
            });
            _copies.Add(new Copy()
            {
                CopyId = 100001,
                BookId = 100000,
                Condition = "Bad",
                Active = false
            });
            _copies.Add(new Copy()
            {
                CopyId = 100002,
                BookId = 100000,
                Condition = "Torn Cover",
                Active = true
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

            _copies.Add(new Copy()
            {
                CopyId = 100003,
                BookId = 100001,
                Condition = "Excellent",
                Active = true
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

        public List<Copy> selectCopiesByBookId(int bookId)
        {
            List<Copy> copies = new List<Copy>();

            foreach (var copy in _copies)
            {
                if(copy.BookId == bookId)
                {
                    copies.Add(copy);
                }
            }

            return copies;
        }
    }
}
