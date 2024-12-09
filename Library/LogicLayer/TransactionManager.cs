using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataAccessLayer;
using DataDomain;

namespace LogicLayer
{
    public class TransactionManager : ITransactionManager
    {
        ITransactionAccessor _transactionAccessor;
        public TransactionManager()
        {
            _transactionAccessor = new TransactionAccessor();
        }
        public TransactionManager(ITransactionAccessor transactionAccessor)
        {
            _transactionAccessor = transactionAccessor;
        }

        public int addTransaction(int userId, string transactionType)
        {
            try
            {
                return _transactionAccessor.insertTransaction(userId, transactionType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void checkinBook(Transaction transaction)
        {
            try
            {
                int id = _transactionAccessor.insertTransaction(transaction.UserId, transaction.TransactionType);
                transaction.TransactionId = id;
                _transactionAccessor.checkinBook(transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void checkoutBook(List<Transaction> transactions)
        {
            try
            {
                int id = _transactionAccessor.insertTransaction(transactions[0].UserId, transactions[0].TransactionType);
                foreach (var transaction in transactions)
                {
                    Transaction temp = transaction;
                    temp.TransactionId = id;
                    _transactionAccessor.checkoutBook(temp);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CopyVM> getCheckedOutCopies(int userId)
        {
            try
            {
                return _transactionAccessor.selectCheckedOutCopies(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CopyVM> getCopiesByTransactionId(int transactionId)
        {
            try
            {
                return _transactionAccessor.selectCopiesByTransactionId(transactionId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Transaction> getTransactionsByUserId(int userId)
        {
            try
            {
                return _transactionAccessor.selectTransactionsByUserId(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
