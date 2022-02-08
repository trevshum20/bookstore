using System;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Models
{
    public class MoviesContext : DbContext
    {
            public MoviesContext(DbContextOptions<MoviesContext> options) : base(options)
        {
            //Leave blank
        }

        public DbSet<bretheren> Responses { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Home" },
                new Category { CategoryId = 2, Name = "School" },
                new Category { CategoryId = 3, Name = "Work" },
                new Category { CategoryId = 4, Name = "Church" }
            );

            _ = mb.Entity<bretheren>().HasData(

                new bretheren
                {
                    TaskID = 1,
                    Task = "Home",
                    Quadrant = 1,
                    DueDate = DateTime.Now,
                    Completed = "False",
                    CategoryId = 1
                }
            );
        }
    }
}
