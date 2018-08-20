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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\admin\source\repos\LedgerCore\Ledger_Core\LedgerCore.Data\LedgerTest.mdf;Integrated Security=True;Connect Timeout=30");
        }
    }
}
