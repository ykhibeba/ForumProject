using Forum.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Post> Posts { get; }
        IRepository<Category> Categories { get; }
        IRepository<Comment> Comments { get; }

        void Save();
    }
}
