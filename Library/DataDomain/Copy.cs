using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataDomain
{
    public class Copy
    {
        public int CopyId { get; set; }
        public int BookId { get; set; }
        public string Condition { get; set; }
        public bool Active { get; set; }
    }
    public class CopyVM : Copy
    {
        public string Name { get; set; }
    }
}
