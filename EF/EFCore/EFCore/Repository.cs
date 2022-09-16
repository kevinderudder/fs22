using Microsoft.EntityFrameworkCore;
using training.models;

namespace training.repository
{
    public class BloggingContext : DbContext
    {
        public BloggingContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                        .ToTable("ModelBlog", schema: "ModelSchema");
            modelBuilder.Entity<Blog>()
                        .Property(b => b.BlogId).HasColumnName("CustomBlogIdentifier");
            modelBuilder.Entity<Blog>()
                        .Property(b => b.Name);
        }
        */
    }
}