using DAL.Models.Entities;
using Microsoft.EntityFrameworkCore;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring one-to-many relationship: One User can borrow many Books
            modelBuilder.Entity<Book>()
                .HasOne(b => b.User) // Each Book belongs to one User
                .WithMany() // A User can borrow many Books
                .HasForeignKey(b => b.UserId); // Foreign key in the Book table

            // Other relationships
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId);

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

            base.OnModelCreating(modelBuilder);
        }
    }
}
