using Library.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess
{
    public class MagazineContext : IdentityDbContext<User>
    {
        public MagazineContext(DbContextOptions<MagazineContext> options) : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<BooksAndAuthor> BooksAndAuthors { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BooksAndAuthor>().HasKey(sc => new { sc.BookId, sc.AuthorId });
            modelBuilder.Entity<User>()
                .HasMany(p => p.Orders)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);
        }
    }
}
