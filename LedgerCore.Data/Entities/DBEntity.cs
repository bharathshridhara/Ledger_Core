using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LedgerCore.Data.Entities
{
    public abstract class DBEntity
    {
        [Required]
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
