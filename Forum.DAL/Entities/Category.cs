using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Forum.DAL.Entities
{
    public class Category
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public Category()
        {
            Posts = new List<Post>();
        }
    }

    public class CategoryConfig: EntityTypeConfiguration<Category>
    {
        public CategoryConfig()
        {
            HasKey(c => c.ID);

            Property(c => c.Title).IsRequired().HasMaxLength(256);
            Property(c => c.Description).IsRequired().HasColumnType("nvarchar(max)");

            HasMany(c => c.Posts).WithRequired(c => c.Category).HasForeignKey(c => c.CategoryID);
        }
    }
}
