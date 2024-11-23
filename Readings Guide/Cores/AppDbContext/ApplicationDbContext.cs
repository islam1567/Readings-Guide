using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Readings_Guide.Cores.Dtos;
using Readings_Guide.Cores.Entities;

namespace Readings_Guide.Cores.AppDbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() { }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Catagory> Catagories { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<FavouriteBookList> FavouriteBookLists { get; set; }
        public DbSet<CurrentluBookList> CurrentluBookLists { get; set; }
        public DbSet<ReadingList> ReadingLists { get; set; }
        public DbSet<ToReadingList> ToReadingLists { get; set; }
    }
}
