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
        public void addBook(Book book, int genreId, int publisherId);
        public void editBook(Book book, Book oldBook, int genreId, int publisherId, int genreId_Old, int publisherId_Old);
        public void addCopy(Copy copy);
        public void editCopy(Copy copy, Copy oldCopy);
        public void deactivateCopy(int copyId);
        public Copy getCopyById(int copyId);
        public CopyVM getCopyVMById(int copyId);
        public void activateCopy(int copyId);
        public List<Genre> getAllGenres();
        public void addGenre(Genre genre);
        public List<Publisher> getAllPublishers();
        public void addPublisher(string name);
        public void addAuthor(string name, int bookId);
        public void editAuthor(int authorId, int bookId);
        public List<Author> getAllAuthors();
        public void addBookAuthor(int authorId, int bookId);
        public List<Book> getBookTable();
    }
}
