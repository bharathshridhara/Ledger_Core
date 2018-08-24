using System;
using System.Collections.Generic;
using System.Text;
using LedgerCore.Data.Constants;

namespace LedgerCore.Data.Entities
{
    public class Transaction : DBEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TransactionType.Type Type { get; set; }
        public int Amount { get; set; }
        public Guid FromAccountId { get; set; }
        public Guid ToAccountId { get; set; }
        public string Recipient { get; set; }

    }
}
