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
        public List<Copy> selectCopiesByBookId(int bookId);
        public void insertBook(Book book);
        public void updateBook(Book book, Book oldBook);
        public void insertCopy(Copy copy);
        public void updateCopy(Copy copy, Copy oldCopy);
        public void deactivateCopy(int copyId);
        public Copy selectCopyById(int copyId);
    }
}
