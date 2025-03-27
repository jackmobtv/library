using DataAccessInterfaces;
using DataAccessLayer;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class BookManager : IBookManager
    {
        private IBookAccessor _bookAccessor;
        public BookManager() 
        { 
            _bookAccessor = new BookAccessor();
        }
        public BookManager(IBookAccessor bookAccessor)
        {
            _bookAccessor = bookAccessor;
        }

        public void activateCopy(int copyId)
        {
            try
            {
                _bookAccessor.activateCopy(copyId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void addAuthor(string name, int bookId)
        {
            try
            {
                _bookAccessor.insertAuthor(name, bookId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void addBook(Book book, int genreId, int publisherId)
        {
            try
            {
                _bookAccessor.insertBook(book, genreId, publisherId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void addBookAuthor(int authorId, int bookId)
        {
            _bookAccessor.insertBookAuthor(authorId, bookId);
        }

        public void addCopy(Copy copy)
        {
            try
            {
                _bookAccessor.insertCopy(copy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void addGenre(Genre genre)
        {
            try
            {
                _bookAccessor.insertGenre(genre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void addPublisher(string name)
        {
            try
            {
                _bookAccessor.insertPublisher(name);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void deactivateCopy(int copyId)
        {
            try
            {
                _bookAccessor.deactivateCopy(copyId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void editAuthor(int authorId, int bookId)
        {
            _bookAccessor.updateAuthor(authorId, bookId);
        }

        public void editBook(Book book, Book oldBook, int genreId, int publisherId, int genreId_Old, int publisherId_Old)
        {
            try
            {
                _bookAccessor.updateBook(book, oldBook, genreId, publisherId, genreId_Old, publisherId_Old);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void editCopy(Copy copy, Copy oldCopy)
        {
            try
            {
                _bookAccessor.updateCopy(copy, oldCopy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Author> getAllAuthors()
        {
            return _bookAccessor.selectAllAuthors();
        }

        public List<Book> getAllBooks()
        {
            try
            {
                return _bookAccessor.selectAllBooks();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Genre> getAllGenres()
        {
            try
            {
                return _bookAccessor.selectAllGenres();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Publisher> getAllPublishers()
        {
            try
            {
                return _bookAccessor.selectAllPublishers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Book getBookById(int bookId)
        {
            Book book = null;
            try
            {
                book = _bookAccessor.selectBookById(bookId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return book;
        }

        public List<Book> getBookTable()
        {
            try
            {
                return _bookAccessor.selectBookTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Copy> getCopiesByBookId(int bookId)
        {
            List<Copy> copies = null;

            try
            {
                copies = _bookAccessor.selectCopiesByBookId(bookId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return copies;
        }

        public Copy getCopyById(int copyId)
        {
            try
            {
                return _bookAccessor.selectCopyById(copyId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
