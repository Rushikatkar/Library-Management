using DAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL.Models
{
    public class LibraryManagementDbContext : DbContext
    {
        public LibraryManagementDbContext(DbContextOptions<LibraryManagementDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BorrowingHistory> BorrowingHistories { get; set; } // Added BorrowingHistory DbSet

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring one-to-many relationship: BorrowingHistory and Book
            modelBuilder.Entity<BorrowingHistory>()
                .HasOne(bh => bh.Book) // Each BorrowingHistory is linked to one Book
                .WithMany(b => b.BorrowingHistories) // A Book can have multiple BorrowingHistories
                .HasForeignKey(bh => bh.BookId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete borrowing history if a book is deleted

            // Configuring one-to-many relationship: BorrowingHistory and User
            modelBuilder.Entity<BorrowingHistory>()
                .HasOne(bh => bh.User) // Each BorrowingHistory is linked to one User
                .WithMany() // A User can have multiple BorrowingHistories
                .HasForeignKey(bh => bh.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete borrowing history if a user is deleted

            // Configuring relationships for Book
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId);

            // Seeding User data
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "Rohan",
                    Email = "rohan@gmail.com",
                    PasswordHash = "$2a$12$TcF6EKxfOoayF7Q6yNlP/.0KkvV5xGgfAZ3XOU1GG.0XjK1J45o1a", // rohan@123
                    Role = "User"
                },
                new User
                {
                    Id = 2,
                    UserName = "Raj",
                    Email = "rajkumar@gmail.com",
                    PasswordHash = "$2b$12$aKsTfYEsr9OzSJi4/SUWCuUNhYGQcD0GDPwaTgJWRaqOgWm9IZubK", //rajkumar@123
                    Role = "Admin" // Example of an admin role
                }
            );



            // Seeding Category data
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Fiction" },
                new Category { Id = 2, Name = "Science" },
                new Category { Id = 3, Name = "History" },
                new Category { Id = 4, Name = "Technology" },
                new Category { Id = 5, Name = "Art" }
            );

            // Seeding Author data with Biography
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "J.K. Rowling", Biography = "British author, best known for writing the Harry Potter series." },
                new Author { Id = 2, Name = "Isaac Newton", Biography = "English mathematician, physicist, and astronomer, widely recognized for his laws of motion and gravity." },
                new Author { Id = 3, Name = "George Orwell", Biography = "English novelist, essayist, journalist, and critic, famous for works like '1984' and 'Animal Farm'." },
                new Author { Id = 4, Name = "Albert Einstein", Biography = "Theoretical physicist best known for developing the theory of relativity and the famous equation E=mc^2." },
                new Author { Id = 5, Name = "Leonardo da Vinci", Biography = "Renaissance polymath known for his contributions to art, science, and engineering, including masterpieces like 'Mona Lisa'." }
            );

            modelBuilder.Entity<Book>().HasData(
               new Book { Id = 1, Title = "Harry Potter and the Philosopher's Stone", AuthorId = 1, CategoryId = 1, PublishedDate = new DateTime(1997, 6, 26), Price = 39.99, Stock = 10, IsBorrowed = false },
               new Book { Id = 2, Title = "Principia Mathematica", AuthorId = 2, CategoryId = 2, PublishedDate = new DateTime(1687, 7, 5), Price = 49.99, Stock = 5, IsBorrowed = false },
               new Book { Id = 3, Title = "1984", AuthorId = 3, CategoryId = 1, PublishedDate = new DateTime(1949, 6, 8), Price = 29.99, Stock = 7, IsBorrowed = false },
               new Book { Id = 4, Title = "Relativity: The Special and General Theory", AuthorId = 4, CategoryId = 2, PublishedDate = new DateTime(1916, 11, 1), Price = 34.99, Stock = 3, IsBorrowed = false },
               new Book { Id = 5, Title = "The Notebooks of Leonardo da Vinci", AuthorId = 5, CategoryId = 5, PublishedDate = new DateTime(1952, 1, 1), Price = 59.99, Stock = 2, IsBorrowed = false }
           );

            // Seeding BorrowingHistory data
            modelBuilder.Entity<BorrowingHistory>().HasData(
                new BorrowingHistory { Id = 1, BookId = 1, UserId = 1, BorrowedDate = DateTime.UtcNow.AddDays(-10), ReturnedDate = DateTime.UtcNow.AddDays(-2), LateFee = 0 },
                new BorrowingHistory { Id = 2, BookId = 2, UserId = 2, BorrowedDate = DateTime.UtcNow.AddDays(-15), ReturnedDate = null, LateFee = 0 } // Not yet returned
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
