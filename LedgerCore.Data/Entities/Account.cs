using System;
using System.Collections.Generic;
using System.Text;

namespace LedgerCore.Data.Entities
{
    public class Account : DBEntity
    {
        public string Name { get; set; }
        public string Currency { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

    }
}
