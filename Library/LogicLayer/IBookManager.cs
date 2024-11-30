using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IBookManager
    {
        public List<Book> getAllBooks();
        public Book getBookById(int bookId);
        public List<Copy> getCopiesByBookId(int bookId);
    }
}
