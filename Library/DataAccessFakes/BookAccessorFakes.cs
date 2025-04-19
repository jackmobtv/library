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
        private List<Genre> _genres;
        private List<Publisher> _publishers;
        private List<Author> _authors;

        public BookAccessorFakes() 
        { 
            _books = new List<Book>();
            _copies = new List<Copy>();
            _genres = new List<Genre>();
            _publishers = new List<Publisher>();
            _authors = new List<Author>();

            _books.Add(new Book()
            {
                BookID = 100000,
                Name = "Tubas and Company",
                Description = "Holiday Tubas",
                Genre = "Horror",
                Author = "Joe Mama",
                Publisher = "Ligma Balls",
            });

            _copies.Add(new Copy()
            {
                CopyID = 100000,
                BookId = 100000,
                Condition = "Good",
                Active = true
            });
            _copies.Add(new Copy()
            {
                CopyID = 100001,
                BookId = 100000,
                Condition = "Bad",
                Active = false
            });
            _copies.Add(new Copy()
            {
                CopyID = 100002,
                BookId = 100000,
                Condition = "Torn Cover",
                Active = true
            });

            _books.Add(new Book()
            {
                BookID = 100001,
                Name = "Macrohard",
                Description = "The Story of Windows ME",
                Genre = "Biography",
                Author = "John Microsoft",
                Publisher = "John Apple",
            });

            _copies.Add(new Copy()
            {
                CopyID = 100003,
                BookId = 100001,
                Condition = "Excellent",
                Active = true
            });

            _books.Add(new Book()
            {
                BookID = 100002,
                Name = "Die Hard: The Movie The Novel The Movie The Novel",
                Description = "Dying Hard or Hardly Dying",
                Genre = "Non-Fiction",
                Author = "John McClaine",
                Publisher = "Hans Gruber",
            });

            _genres.Add(new Genre()
            {
                Name = "Genre1",
                Description = "Genre1Description"
            });
            _genres.Add(new Genre()
            {
                Name = "Genre2",
                Description = "Genre2Description"
            });
            _genres.Add(new Genre()
            {
                Name = "Genre3",
                Description = "Genre3Description"
            });

            _publishers.Add(new Publisher() { Name = "Publisher1" });
            _publishers.Add(new Publisher() { Name = "Publisher2" });
            _publishers.Add(new Publisher() { Name = "Publisher3" });

            _authors.Add(new Author()
            {
                AuthorID = 1000000,
                Name = "Author1"
            });
            _authors.Add(new Author()
            {
                AuthorID = 1000001,
                Name = "Author2"
            });
            _authors.Add(new Author()
            {
                AuthorID = 1000002,
                Name = "Author3"
            });
        }

        public void activateCopy(int copyId)
        {
            foreach (var copy in _copies)
            {
                if (copy.CopyID == copyId)
                {
                    copy.Active = true;
                    return;
                }
            }
            throw new ArgumentException("Copy Not Found");
        }

        public void deactivateCopy(int copyId)
        {
            foreach (var copy in _copies)
            {
                if(copy.CopyID == copyId)
                {
                    copy.Active = false;
                    return;
                }
            }
            throw new ArgumentException("Copy Not Found");
        }

        public List<Book> selectBookTable()
        {
            return _books;
        }

        public void insertAuthor(string name, int bookId)
        {
            _authors.Add(new Author()
            {
                Name = name,
            });
        }

        public void insertBook(Book book, int genreId, int publisherId)
        {
            _books.Add(book);
        }

        public void insertBookAuthor(int authorId, int bookId)
        {
            throw new NotImplementedException();
        }

        public void insertCopy(Copy copy)
        {
            _copies.Add(copy);
        }

        public void insertGenre(Genre genre)
        {
            _genres.Add(genre);
        }

        public void insertPublisher(string name)
        {
            _publishers.Add(new Publisher() { Name = name });
        }

        public List<Author> selectAllAuthors()
        {
            return _authors;
        }

        public List<Book> selectAllBooks()
        {
            return _books;
        }

        public List<Genre> selectAllGenres()
        {
            return _genres;
        }

        public List<Publisher> selectAllPublishers()
        {
            return _publishers;
        }

        public Book selectBookById(int bookId)
        {
            foreach (var book in _books)
            {
                if(book.BookID == bookId)
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

        public Copy selectCopyById(int copyId)
        {
            foreach (var copy in _copies)
            {
                if (copy.CopyID == copyId)
                {
                    return copy;
                }
            }
            throw new ArgumentException("Copy Not Found");
        }

        public void updateAuthor(int authorId, int bookId)
        {
            for (int i = 0; i < _authors.Count; i++)
            {
                if (_authors[i].AuthorID == authorId)
                {
                    return;
                }
            }
            throw new ArgumentException("Author Not Found");
        }

        public void updateBook(Book book, Book oldBook, int genreId, int publisherId, int genreId_Old, int publisherId_Old)
        {
            for (int i = 0; i < _books.Count; i++)
            {
                if (_books[i].BookID == oldBook.BookID)
                {
                    _books[i] = book;
                    return;
                }
            }
            throw new ArgumentException("Book Not Found");
        }

        public void updateCopy(Copy copy, Copy oldCopy)
        {
            for (int i = 0; i < _copies.Count; i++)
            {
                if (_copies[i].CopyID == oldCopy.CopyID)
                {
                    _copies[i] = copy;
                    return;
                }
            }
            throw new ArgumentException("Book Not Found");
        }
    }
}
