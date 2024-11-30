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
    }
}
