using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataAccessInterfaces
{
    public interface ITransactionAccessor
    {
        public List<CopyVM> selectCheckedOutCopies(int userId);
        public int insertTransaction(int userId, string transactionType);
        public void checkoutBook(Transaction transaction);
        public void checkinBook(Transaction transaction);
    }
}
