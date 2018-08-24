using System;
using System.Collections.Generic;
using System.Text;

namespace LedgerCore.Data.Entities
{
    public class Client : DBEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ApiKey { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
