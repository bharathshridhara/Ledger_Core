using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LedgerCore.Data.Constants;
using LedgerCore.Data.Entities;

namespace LedgerCore.ViewModels
{
    public class TransactionDTO :BaseDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public TransactionType.Type Type { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }
        public Guid? ToAccountId { get; set; }
        public string Recipient { get; set; }
    }
}
