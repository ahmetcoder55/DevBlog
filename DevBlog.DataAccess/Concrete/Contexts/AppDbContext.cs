using DevBlog.Core.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevBlog.DataAccess.Concrete.Contexts
{
    public class AppDbContext: IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Article> Articles => Set<Article>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<ArticleTag> ArticleTags => Set<ArticleTag>();
        public DbSet<Comment> Comments => Set<Comment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ArticleTag>()
                .HasKey(at => new { at.ArticleId, at.TagId });

            modelBuilder.Entity<ArticleTag>()
                .HasOne(at => at.Article)
                .WithMany(a => a.ArticleTags)
                .HasForeignKey(at => at.ArticleId);

            modelBuilder.Entity<ArticleTag>()
                .HasOne(at => at.Tag)
                .WithMany(t => t.ArticleTags)
                .HasForeignKey(at => at.TagId);

            modelBuilder.Entity<Comment>(builder =>
            {
                builder.Property(c => c.AuthorName)
                       .IsRequired()
                       .HasMaxLength(50);

                builder.Property(c => c.AuthorEmail)
                       .IsRequired()
                       .HasMaxLength(100);

                builder.Property(c => c.Content)
                       .IsRequired()
                       .HasMaxLength(500);

                builder.HasOne(c => c.Article)
                       .WithMany(a => a.Comments)
                       .HasForeignKey(c => c.ArticleId)
                       .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Article>().HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Comment>().HasQueryFilter(cm => !cm.IsDeleted);
        }
    }
}
