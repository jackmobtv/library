using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class TabController
    {
        public bool BookList { get; set; } = true;
        public bool CheckoutList { get; set; } = false;
        public bool CheckedOutList { get; set; } = false;
        public bool MemberList { get; set; } = false;
        public bool BookManagement { get; set; } = false;
    }
}
