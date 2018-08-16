using Forum.DAL.EF;
using Forum.DAL.Entities;
using Forum.DAL.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace Forum.DAL.Repositories
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private ForumContext _db;
        private DbSet<T> _dbSet;

        public EFRepository(ForumContext context)
        {
            _db = context;
            _dbSet = _db.Set<T>();
        }

        public void Create(T item)
        {
            _dbSet.Add(item);
        }

        public void Delete(int id)
        {
            var delete = GetById(id);

            if (delete != null)
                _dbSet.Remove(delete);
        }

        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(T item)
        {
            _dbSet.AddOrUpdate(item);
        }
    }
}
