using DataAccessFakes;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;
using System.Runtime;

namespace LogicLayerTests
{
    [TestClass]
    public class BookManagerTests
    {
        BookManager? _bookManager;

        [TestInitialize]
        public void testStartup()
        {
            _bookManager = new BookManager(new BookAccessorFakes());
        }

        [TestMethod]
        public void testGetAllBooks()
        {
            int expectedValue = 3;

            int actualValue = _bookManager.getAllBooks().Count();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void testGetBookById()
        {
            int bookId = 100000;
            string expectedValue = "Tubas and Company";

            string actualValue = _bookManager.getBookById(bookId).Name;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void testGetCopiesByBookId()
        {
            int bookId = 100000;
            int expectedValue = 3;

            int actualValue = _bookManager.getCopiesByBookId(bookId).Count();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void testAddBook()
        {
            Book book = new Book()
            {
                Name = "TestName",
                Description = "TestDescription",
                Genre = "TestGenre",
                Author = "TestAuthor",
                Publisher = "TestPublisher"
            };
            int expectedValue = 4;
            int actualValue;
            
            _bookManager.addBook(book);
            actualValue = _bookManager.getAllBooks().Count;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void testEditBook()
        {
            Book book = new Book()
            {
                BookId = 100002,
                Name = "TestName2",
                Description = "TestDescription2",
                Genre = "TestGenre2",
                Author = "TestAuthor2",
                Publisher = "TestPublisher2"
            };

            Book oldBook = new Book()
            {
                BookId = 100002,
                Name = "Die Hard: The Movie The Novel The Movie The Novel",
                Description = "Dying Hard or Hardly Dying",
                Genre = "Non-Fiction",
                Author = "John McClaine",
                Publisher = "Hans Gruber",
            };

            _bookManager.editBook(book, oldBook);
            Book expectedBook = _bookManager.getBookById(book.BookId);

            Assert.AreEqual(book.Name, expectedBook.Name);
        }

        [TestMethod]
        public void testAddCopy()
        {
            Copy copy = new Copy()
            {
                CopyId = 100003,
                BookId = 100000,
                Condition = "Good",
                Active = true
            };
            int expectedValue = 4;
            int actualValue;

            _bookManager.addCopy(copy);
            actualValue = _bookManager.getCopiesByBookId(100000).Count;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void testEditCopy()
        {
            Copy copy = new Copy()
            {
                CopyId = 100000,
                BookId = 100000,
                Condition = "Test",
                Active = true
            };

            Copy oldCopy = new Copy()
            {
                CopyId = 100000,
                BookId = 100000,
                Condition = "Good",
                Active = true
            };

            _bookManager.editCopy(copy, oldCopy);
            Copy expectedCopy = _bookManager.getCopyById(copy.CopyId);

            Assert.AreEqual(copy.Condition, expectedCopy.Condition);
        }

        [TestMethod]
        public void testDeactivateCopy()
        {
            const int copyId = 100000;
            
            _bookManager.deactivateCopy(copyId);

            Assert.IsFalse(_bookManager.getCopyById(copyId).Active);
        }

        [TestMethod]
        public void testGetCopyById()
        {
            const int copyId = 100000;
            const string expectedValue = "Good";
            string actualValue;

            actualValue = _bookManager.getCopyById(copyId).Condition;

            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
