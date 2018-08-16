using System;
using System.Data.Entity.ModelConfiguration;

namespace Forum.DAL.Entities
{
    public class Comment
    {
        public int ID { get; set; }

        public int PostID { get; set; }

        public Post Post { get; set; }

        public DateTime DateTime { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Body { get; set; }
    }

    public class CommentConfig: EntityTypeConfiguration<Comment>
    {
        public CommentConfig()
        {
            HasKey(c => c.ID);

            Property(c => c.DateTime).IsRequired().HasColumnType("datetime");
            Property(c => c.Name).IsRequired().HasMaxLength(128);
            Property(c => c.Body).IsRequired().HasColumnType("nvarchar(max)");
            Property(c => c.UserName).IsRequired().HasMaxLength(128);
            
        }
    }
}
