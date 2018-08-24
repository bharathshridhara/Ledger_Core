using System;
using System.Collections.Generic;
using System.Text;

namespace LedgerCore.Data.Entities
{
    public class Currency : DBEntity
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
