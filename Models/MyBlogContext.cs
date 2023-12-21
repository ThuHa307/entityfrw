using Microsoft.EntityFrameworkCore;

namespace entityfrw.models {
    public class MyBlogContext : DbContext {
        public DbSet<Article> articles { get; set; }

        public MyBlogContext (DbContextOptions<MyBlogContext> options) : base(options) { }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.Entity<Article> ().ToTable ("Article");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}