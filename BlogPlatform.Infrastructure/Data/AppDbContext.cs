using Microsoft.EntityFrameworkCore;
using BlogPlatform.Core.Entities;

namespace BlogPlatform.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureRelationshipsAndConstraints(modelBuilder);
        }

        private static void ConfigureRelationshipsAndConstraints(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Comment>()
                .Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Comment>()
                .Property(c => c.Email)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<Comment>()
                .Property(c => c.Content)
                .HasMaxLength(1000)
                .IsRequired();
            }
        }
}