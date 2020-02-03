using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shelved.Models;

namespace Shelved.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<Book> Book { get; set; }
        public DbSet<BookGenre> BookGenre { get; set; }
        public DbSet<GenresForBooks> GenresForBook { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<MovieGenre> MovieGenre { get; set; }
        public DbSet<GenresForMovies> GenresForMovie { get; set; }
        public DbSet<CD> CD { get; set; }
        public DbSet<CDGenre> CdGenre { get; set; }
        public DbSet<GenresForCDs> GetGenresForCD { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Create a new user for Identity Framework
            ApplicationUser user = new ApplicationUser
            {
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff"
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);


            // create book genres
            modelBuilder.Entity<BookGenre>()
                .HasOne(bg => bg.Book)
                .WithMany(b => b.BookGenres)
                .HasForeignKey(bg => bg.BookId);
            modelBuilder.Entity<BookGenre>()
                .HasOne(bg => bg.GenresForBooks)
                .WithMany(bo => bo.BookGenres)
                .HasForeignKey(bg => bg.GenreId);

            modelBuilder.Entity<GenresForBooks>().HasData(

                new GenresForBooks
                {
                    Id = 1,
                    Description = "Science Fiction"
                },
                new GenresForBooks
                {
                    Id = 2,
                    Description = "Sports"
                },
                new GenresForBooks
                {
                    Id = 3,
                    Description = "Comedy"
                },
                new GenresForBooks
                {
                    Id = 4,
                    Description = "Romance"
                },
                new GenresForBooks
                {
                    Id = 5,
                    Description = "Thriller"
                },
                new GenresForBooks
                {
                    Id = 6,
                    Description = "Mystery"
                },
                new GenresForBooks
                {
                    Id = 7,
                    Description = "Children's"
                },
                new GenresForBooks
                {
                    Id = 8,
                    Description = "Religion"
                },
                      new GenresForBooks
                      {
                          Id = 9,
                          Description = "CookBook"
                      },
                       new GenresForBooks
                       {
                           Id = 10,
                           Description = "Travel"
                       });



            // create movie genres
            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(mg => mg.MovieId);
            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.GenresForMovies)
                .WithMany(mo => mo.MovieGenres)
                .HasForeignKey(mg => mg.GenreId);

            modelBuilder.Entity<GenresForMovies>().HasData(

                new GenresForMovies
                {
                    Id = 1,
                    Description = "Comedy"
                },
                new GenresForMovies
                {
                    Id = 2,
                    Description = "Action"
                },
                new GenresForMovies
                {
                    Id = 3,
                    Description = "Romance"
                },
                new GenresForMovies
                {
                    Id = 4,
                    Description = "Thriller"
                },
                new GenresForMovies
                {
                    Id = 5,
                    Description = "Drama"
                },
                new GenresForMovies
                {
                    Id = 6,
                    Description = "Western"
                },
                new GenresForMovies
                {
                    Id = 7,
                    Description = "Science Fiction"
                },
                new GenresForMovies
                {
                    Id = 8,
                    Description = "Horror"
                });


            // create cd genres
            modelBuilder.Entity<CDGenre>()
                .HasOne(cg => cg.CD)
                .WithMany(c => c.CDGenres)
                .HasForeignKey(cg => cg.CDId);
            modelBuilder.Entity<CDGenre>()
                .HasOne(cg => cg.GenresForCDs)
                .WithMany(cd => cd.CDGenres)
                .HasForeignKey(cg => cg.GenreId);

            modelBuilder.Entity<GenresForCDs>().HasData(

                new GenresForCDs
                {
                    Id = 1,
                    Description = "Rock"
                },
                new GenresForCDs
                {
                    Id = 2,
                    Description = "Pop"
                },
                new GenresForCDs
                {
                    Id = 3,
                    Description = "Folk"
                },
                new GenresForCDs
                {
                    Id = 4,
                    Description = "Rap"
                },
                new GenresForCDs
                {
                    Id = 5,
                    Description = "Jazz"
                },
                new GenresForCDs
                {
                    Id = 6,
                    Description = "Country"
                },
                new GenresForCDs
                {
                    Id = 7,
                    Description = "Blues"
                },
                new GenresForCDs
                {
                    Id = 8,
                    Description = "Classical"
                });


            // create books
            Book book1 = new Book()
            {
                Id = 1,
                Title = "One For The Money",
                Author = "Janet Evanovich",
                Year = "1994",
                IsRead = true,
                ApplicationUserId = user.Id
            };
            modelBuilder.Entity<Book>().HasData(book1);

            Book book2 = new Book()
            {
                Id = 2,
                Title = "Fever Pitch",
                Author = "Nick Hornby",
                Year = "1992",
                IsRead = true,
                ApplicationUserId = user.Id
            };
            modelBuilder.Entity<Book>().HasData(book2);

            Book book3 = new Book()
            {
                Id = 3,
                Title = "The Code of the Woosters",
                Author = "P.G. Wodehouse",
                Year = "1938",
                IsRead = true,
                ApplicationUserId = user.Id
            };
            modelBuilder.Entity<Book>().HasData(book3);


            // create movies
            Movie movie1 = new Movie()
            {
                Id = 1,
                Title = "Dr. Doolittle",
                Year = "2020",
                IsWatched = true,
                ApplicationUserId = user.Id
            };
            modelBuilder.Entity<Movie>().HasData(movie1);

            Movie movie2 = new Movie()
            {
                Id = 2,
                Title = "Captain America",
                Year = "2011",
                IsWatched = true,
                ApplicationUserId = user.Id
            };
            modelBuilder.Entity<Movie>().HasData(movie2);

            Movie movie3 = new Movie()
            {
                Id = 3,
                Title = "Big Business",
                Year = "1988",
                IsWatched = true,
                ApplicationUserId = user.Id
            };
            modelBuilder.Entity<Movie>().HasData(movie3);


            // create cds
            CD cd1 = new CD()
            {
                Id = 1,
                Title = "Sheryl Crow",
                Artist = "Sheryl Crow",
                Year = "1996",
                IsHeard = true,
                ApplicationUserId = user.Id
            };
            modelBuilder.Entity<CD>().HasData(cd1);

            CD cd2 = new CD()
            {
                Id = 2,
                Title = "Americana",
                Artist = "The Offspring",
                Year = "1998",
                IsHeard = true,
                ApplicationUserId = user.Id
            };
            modelBuilder.Entity<CD>().HasData(cd2);

            CD cd3 = new CD()
            {
                Id = 3,
                Title = "In Your Honor",
                Artist = "Foo Fighters",
                Year = "2005",
                IsHeard = true,
                ApplicationUserId = user.Id
            };
            modelBuilder.Entity<CD>().HasData(cd3);


        }


    }
}
