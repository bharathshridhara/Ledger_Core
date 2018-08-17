using System;
using System.Collections.Generic;
using System.Text;
using LedgerCore.Data.Entities;
using LedgerCore.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LedgerCore.Data.Repositories
{
    class Repository : IRepository<DBEntity>
    {
        private DbContext _dbContext;
        public Repository(DbContext context)
        {
            _dbContext = context;
        }
        public IEnumerable<T> GetAll<T>()
        {
            
        }

        public DBEntity GetOne(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Insert<T>(T t)
        {
            throw new NotImplementedException();
        }

        public void UpdateOne<T>(T t)
        {
            throw new NotImplementedException();
        }

        public void Upsert<T>(T t)
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T t)
        {
            throw new NotImplementedException();
        }
    }
}
