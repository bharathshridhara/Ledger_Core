using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LedgerCore.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace LedgerCore.Data.Repositories
{
    public class DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        public ClaimsPrincipal User { get; set; }

        public DBContext(DbContextOptions options) : base(options)
        {    
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            return await Users.Where(user => user.Email.Equals(email) && user.Password.Equals(password)).FirstOrDefaultAsync();
        }

        public override int SaveChanges()
        {
            AddTimeStamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AddTimeStamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimeStamps()
        {
            var entities = ChangeTracker.Entries().Where(e => e.Entity is DBEntity
                                                              && (e.State == EntityState.Added ||
                                                                  e.State == EntityState.Modified));
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((DBEntity) entity.Entity).Created = DateTime.Now.ToUniversalTime();
                    ((DBEntity)entity.Entity).Modified = DateTime.Now.ToUniversalTime();
                }
                else if (entity.State == EntityState.Modified)
                {
                    ((DBEntity)entity.Entity).Modified = DateTime.Now.ToUniversalTime();
                }
                var name = User?.Identity?.Name;
                if (string.IsNullOrEmpty(name))
                    ((DBEntity) entity.Entity).ModifiedBy = "Anonymous";
                else
                    ((DBEntity) entity.Entity).ModifiedBy = name;
                
            }
        }
    }
}
