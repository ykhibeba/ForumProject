using System.Collections.Generic;
using Forum.DAL.EF;
using Forum.DAL.Entities;
using Forum.DAL.Interfaces;
using System.Data.Entity;
using System;

namespace Forum.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ForumContext db;

        private EFRepository<Category> categoryRepository;

        private EFRepository<Post> postRepository;

        private EFRepository<Comment> commentRepository;

        public EFUnitOfWork(string connection)
        {
            db = new ForumContext(connection);
        }

        public IRepository<Post> Posts
        {
            get
            {
                if (postRepository == null)
                    postRepository = new EFRepository<Post>(db);
                return postRepository;
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new EFRepository<Category>(db);
                return categoryRepository;
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                if (commentRepository == null)
                    commentRepository = new EFRepository<Comment>(db);
                return commentRepository;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~EFUnitOfWork()
        {
            Dispose(false);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
