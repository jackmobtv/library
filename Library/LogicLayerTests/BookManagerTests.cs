using DataAccessFakes;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
