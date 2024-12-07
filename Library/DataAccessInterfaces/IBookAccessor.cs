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
        public void insertBook(Book book, int genreId, int publisherId);
        public void updateBook(Book book, Book oldBook, int genreId, int publisherId, int genreId_Old, int publisherId_Old);
        public void insertCopy(Copy copy);
        public void updateCopy(Copy copy, Copy oldCopy);
        public void deactivateCopy(int copyId);
        public Copy selectCopyById(int copyId);
        public void activateCopy(int copyId);
        public List<Genre> selectAllGenres();
        public void insertGenre(Genre genre);
        public List<Publisher> selectAllPublishers();
        public void insertPublisher(string name);
        public void insertAuthor(string name, int bookId);
        public void updateAuthor(int authorId, int bookId);
        public List<Author> selectAllAuthors();
        public void insertBookAuthor(int authorId, int bookId);
        public List<Book> selectBookTable();
    }
}
