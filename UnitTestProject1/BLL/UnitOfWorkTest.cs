using Forum.DAL.Entities;
using Forum.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Forum.Tests.BLL
{
    public class UnitOfWorkTest : IUnitOfWork
    {
        private RepositoryCategory categoryRepository;

        private RepositoryPost postRepository;

        private RepositoryComments commentRepository;

        public IRepository<Post> Posts
        {
            get
            {
                if (postRepository == null)
                    postRepository = new RepositoryPost();
                return postRepository;
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new RepositoryCategory();
                return categoryRepository;
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                if (commentRepository == null)
                    commentRepository = new RepositoryComments();
                return commentRepository;
            }
        }

        public void Dispose()
        {
            return;
        }

        public void Save()
        {
            return;
        }
    }
}
