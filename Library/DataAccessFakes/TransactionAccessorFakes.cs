using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;

namespace DataAccessFakes
{
    public class TransactionAccessorFakes : ITransactionAccessor
    {
        private List<Transaction> _transactions;
        private List<CopyVM> _copies;

        public TransactionAccessorFakes() 
        {
            _transactions = new List<Transaction>();
            _copies = new List<CopyVM>();

            _transactions.Add(new Transaction()
            {
                TransactionId = 100000,
                TransactionType = "CHECKIN",
                UserId = 100000,
                Active = true,
                CopyId = 100000
            });
            _transactions.Add(new Transaction()
            {
                TransactionId = 100001,
                TransactionType = "CHECKIN",
                UserId = 100000,
                Active = true,
                CopyId = 100001
            });
            _transactions.Add(new Transaction()
            {
                TransactionId = 100002,
                TransactionType = "CHECKOUT",
                UserId = 100002,
                Active = false,
                CopyId = 100002
            });

            _copies.Add(new CopyVM()
            {
                CopyId = 100000,
                BookId = 100000,
                Condition = "Good",
                Active = true
            });
            _copies.Add(new CopyVM()
            {
                CopyId = 100001,
                BookId = 100000,
                Condition = "Bad",
                Active = false
            });
            _copies.Add(new CopyVM()
            {
                CopyId = 100002,
                BookId = 100000,
                Condition = "Torn Cover",
                Active = true
            });
        }

        public void checkinBook(Transaction transaction)
        {
            foreach (var copy in _copies)
            {
                if (copy.CopyId == transaction.CopyId)
                {
                    copy.Active = true;
                }
            }
        }

        public void checkoutBook(Transaction transaction)
        {
            foreach (var copy in _copies)
            {
                if (copy.CopyId == transaction.CopyId)
                {
                    copy.Active = false;
                }
            }
        }

        public int insertTransaction(int userId, string transactionType)
        {
            Transaction transaction = new Transaction() { 
                UserId = userId, 
                TransactionType = transactionType,
                TransactionId = 100000
            };
            _transactions.Add(transaction);
            return transaction.TransactionId;
        }

        public List<CopyVM> selectCheckedOutCopies(int userId)
        {
            List<CopyVM> copies = new List<CopyVM>();

            foreach (var transaction in _transactions)
            {
                if (transaction.UserId == userId)
                {
                    foreach (var copy in _copies)
                    {
                        if(copy.CopyId == transaction.CopyId && transaction.Active)
                        {
                            copies.Add(copy);
                        }
                    }
                }
            }

            return copies;
        }
    }
}
