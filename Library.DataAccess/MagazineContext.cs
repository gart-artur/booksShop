using Library.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Library.DataAccess
{
    public class MagazineContext : IdentityDbContext<IdentityUser>
    {
      

        public MagazineContext(DbContextOptions<MagazineContext> options):base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BooksAndAuthor> BooksAndAuthors { get; set; }
        public DbSet<User> Users { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BooksAndAuthor>().HasKey(sc => new {sc.BookId, sc.AuthorId });
            
        }

    }
}
