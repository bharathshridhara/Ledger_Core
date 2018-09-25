using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LedgerCore.Data.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LedgerCore.ViewModels
{
    public class AccountDTO : BaseDto
    {
        [Required]
        public Guid UserId { get; set; }

        [MaxLength(15)][Required]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        public string CurrencyISO { get; set; }

        public float InterestRate { get; set; }

        public Link TransactionsURL { get; set; }
    }
}
