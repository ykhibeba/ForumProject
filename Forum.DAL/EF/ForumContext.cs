using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;
using Forum.DAL.Entities;

namespace Forum.DAL.EF
{
    public class ForumContext: DbContext
    {
        //Table posts.
        public DbSet<Post> Posts { get; set; }

        //Table categories.
        public DbSet<Category> Categories { get; set; }

        //Table comments.
        public DbSet<Comment> Comments { get; set; }

        public ForumContext(string connectingString) : base(connectingString) {  }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ForumContext>(null);
            modelBuilder.Configurations.Add(new CommentConfig());
            modelBuilder.Configurations.Add(new CategoryConfig());
            modelBuilder.Configurations.Add(new PostConfig());
        }
    }
}
