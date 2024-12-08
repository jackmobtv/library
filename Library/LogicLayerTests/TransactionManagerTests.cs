using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer;
using DataAccessFakes;
using DataDomain;

namespace LogicLayerTests
{
    [TestClass]
    public class TransactionManagerTests
    {
        TransactionManager? _transactionManager;

        [TestInitialize]
        public void testStartup()
        {
            _transactionManager = new TransactionManager(new TransactionAccessorFakes());
        }

        [TestMethod]
        public void testGetCheckedOutCopies()
        {
            int expectedValue = 2;
            int actualValue;

            actualValue = _transactionManager.getCheckedOutCopies(100000).Count;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void testAddTransaction()
        {
            int expectedValue = 100000;
            int actualValue;

            actualValue = _transactionManager.addTransaction(100000, "CHECKOUT");
            
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void testCheckinBook()
        {
            int copyId = 100002;
            int userId = 100000;
            string transactionType = "CHECKOUT";
            Transaction transaction = new Transaction()
            {
                CopyId = copyId,
                UserId = userId,
                TransactionType = transactionType
            };

            _transactionManager.checkinBook(transaction);
        }

        [TestMethod]
        public void testCheckoutBook()
        {
            int copyId = 100002;
            int userId = 100000;
            string transactionType = "CHECKOUT";
            List<Transaction> transaction = new List<Transaction>();

            transaction.Add(new Transaction()
            {
                CopyId = copyId,
                UserId = userId,
                TransactionType = transactionType
            });

            _transactionManager.checkoutBook(transaction);
        }

        [TestMethod]
        public void testGetTransactionsByUserId()
        {
            int expectedValue = 2;
            int id = 100000;
            int actualValue;

            actualValue = _transactionManager.getTransactionsByUserId(id).Count;

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void testGetCopiesByTransactionId()
        {
            int id = 100000;
            int expectedValue = 1;
            int actualValue;

            actualValue = _transactionManager.getCopiesByTransactionId(id).Count;

            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
