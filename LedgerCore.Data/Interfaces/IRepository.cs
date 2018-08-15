using System;
using System.Collections.Generic;
using System.Text;
using LedgerCore.Data.Entities;

namespace LedgerCore.Data.Interfaces
{
    interface IRepository<T> where T : DBEntity
    {
        T GetOne(Guid id);
        IEnumerable<T> GetAll<T>();
        void UpdateOne<T>(T t);
        void Upsert<T>(T t);
        void Insert<T>(T t);
        void Delete<T>(T t);
    }
}
