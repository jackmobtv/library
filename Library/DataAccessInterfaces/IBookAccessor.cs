using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IBookAccessor
    {
        public List<Book> selectAllBooks();
        public Book selectBookById(int bookId);
    }
}
