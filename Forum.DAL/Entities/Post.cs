using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Forum.DAL.Entities
{
    public class Post
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string CreatorName { get; set; }

        public string UserName { get; set; }

        public DateTime DateTime { get; set; }

        public string Body { get; set; }

        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public Post()
        {
            Comments = new List<Comment>();
        }
    }

    public class PostConfig : EntityTypeConfiguration<Post>
    {
        public PostConfig()
        {
            HasKey(p => p.ID);

            Property(p => p.Title).IsRequired().HasMaxLength(256);
            Property(p => p.CreatorName).IsRequired().HasMaxLength(128);
            Property(p => p.DateTime).IsRequired().HasColumnType("datetime");
            Property(p => p.Body).IsRequired().HasColumnType("nvarchar(max)");
            Property(p => p.UserName).IsRequired().HasMaxLength(128);

            HasMany(p => p.Comments).WithRequired(p => p.Post).HasForeignKey(p => p.PostID);
            
        }
    }
}
