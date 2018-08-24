using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LedgerCore.Data.Entities
{
    public class Account : DBEntity
    {
        public string Name { get; set; }
        [MaxLength(250)]
        
        public string Description { get; set; }
        public Guid CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public float InterestRate { get; set; }

    }
}
