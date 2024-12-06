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
        public void addBook(Book book);
        public void editBook(Book book, Book oldBook);
        public void addCopy(Copy copy);
        public void editCopy(Copy copy, Copy oldCopy);
        public void deactivateCopy(int copyId);
        public Copy getCopyById(int copyId);
    }
}
