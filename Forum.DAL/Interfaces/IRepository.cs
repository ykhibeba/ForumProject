using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Func<T, bool> predicate);
        T GetById(int id);
        void Create(T item);
        void Delete(int id);
        void Update(T item);
    }
}
