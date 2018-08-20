using System;
using System.Collections.Generic;
using System.Text;

namespace LedgerCore.Data.Entities
{
    public class User : DBEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}
