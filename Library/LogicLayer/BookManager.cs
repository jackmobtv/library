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

        public void addBook(Book book)
        {
            try
            {
                _bookAccessor.insertBook(book);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        public void deactivateCopy(int copyId)
        {
            _bookAccessor.deactivateCopy(copyId);
        }

        public void editBook(Book book, Book oldBook)
        {
            try
            {
                _bookAccessor.updateBook(book, oldBook);
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
