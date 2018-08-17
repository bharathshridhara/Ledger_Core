using System;
using System.Collections.Generic;
using System.Text;
using LedgerCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LedgerCore.Data.Repositories
{
    public class DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
