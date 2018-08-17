using System;
using System.Collections.Generic;
using System.Text;

namespace LedgerCore.Data.Entities
{
    public class Account : DBEntity
    {
        public string Name { get; set; }
        public string Currency { get; set; }

    }
}
