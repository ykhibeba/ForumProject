using Forum.DAL.Entities;
using Forum.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Tests.BLL
{
    public class RepositoryPost : IRepository<Post>
    {
        private List<Post> posts;
        private IEnumerable<Post> ListPosts()
        {
            return new Post[]
            {
                new Post {CategoryID = 1, Title = "Post1", Body = "Post1_Category1", ID = 1, UserName = "Admin"},
                new Post {CategoryID = 1, Title = "Post2", Body = "Post2_Category1", ID = 2, UserName = "User"},
                new Post {CategoryID = 2, Title = "Post1", Body = "Post1_Category2", ID = 3, UserName = "User"}
            };
        }

        public RepositoryPost()
        {
            posts = new List<Post>(ListPosts());
        }

        public void Create(Post item)
        {
            posts.Add(item);
        }

        public void Delete(int id)
        {
            var delete = GetById(id);
            if (delete != null)
                posts.Remove(delete);
        }

        public IEnumerable<Post> Get(Func<Post, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            return posts;
        }

        public Post GetById(int id)
        {
            return posts.Find(x => x.ID == id);
        }

        public void Update(Post item)
        {
            var index = posts.FindIndex(x => x.ID == item.ID);
            posts.RemoveAt(index);
            posts.Insert(index, item);
        }
    }

    public class RepositoryCategory : IRepository<Category>
    {
        private List<Category> categories;
        private IEnumerable<Category> ListCategories()
        {
            return new Category[]
                {
                new Category {ID=1, Title = "First",Description="FirstDesc"},
                new Category {ID=2, Title = "Second",Description="FirstDesc"},
                new Category {ID=3, Title = "Third",Description="FirstDesc"},
                new Category {ID=4, Title = "Fourth",Description="FirstDesc"}
                };
        }

        public RepositoryCategory()
        {
            categories = new List<Category>(ListCategories());
        }

        public void Create(Category item)
        {
            categories.Add(item);
        }

        public void Delete(int id)
        {
            var delete = GetById(id);
            if (delete != null)
                categories.Remove(delete);
        }

        public IEnumerable<Category> Get(Func<Category, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAll()
        {
            return categories;
        }

        public Category GetById(int id)
        {
            return categories.Find(x => x.ID == id);
        }

        public void Update(Category item)
        {
            var index = categories.FindIndex(x => x.ID == item.ID);
            categories.RemoveAt(index);
            categories.Insert(index, item);
        }
    }

    public class RepositoryComments : IRepository<Comment>
    {
        private List<Comment> comments;
        private IEnumerable<Comment> ListComments()
        {
            return new Comment[]
            {
                new Comment { ID = 1, PostID = 1, Body = "Comment1_Post_1"},
                new Comment {ID = 2, PostID = 1, Body = "Comment2_Post_1"},
                new Comment {ID = 3, PostID = 2, Body = "Comment1_Post_2"}
            };
        }

        public RepositoryComments()
        {
            comments = new List<Comment>(ListComments());
        }

        public void Create(Comment item)
        {
            comments.Add(item);
        }

        public void Delete(int id)
        {
            var delete = GetById(id);
            if (delete != null)
                comments.Remove(delete);
        }

        public IEnumerable<Comment> Get(Func<Comment, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> GetAll()
        {
            return comments;
        }

        public Comment GetById(int id)
        {
            return comments.Find(x => x.ID == id);
        }

        public void Update(Comment item)
        {
            var index = comments.FindIndex(x => x.ID == item.ID);
            comments.RemoveAt(index);
            comments.Insert(index, item);
        }
    }
}
