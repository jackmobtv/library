﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int UserId { get; set; }
        public string TransactionType { get; set; }
        public int CopyId { get; set; }
        public bool Active { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
