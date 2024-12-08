using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace LogicLayer
{
    public interface ITransactionManager
    {
        public List<CopyVM> getCheckedOutCopies(int userId);
        public int addTransaction(int userId, string transactionType);
        public void checkoutBook(List<Transaction> transactions);
        public void checkinBook(Transaction transaction);
    }
}
