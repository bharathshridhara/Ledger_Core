using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LedgerCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LedgerCore.Data.Repositories
{
    public class DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Transactions { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        public DBContext(DbContextOptions options) : base(options)
        {
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            return await Users.Where(user => user.Email.Equals(email) && user.Password.Equals(password)).FirstOrDefaultAsync();
        }
    }
}
