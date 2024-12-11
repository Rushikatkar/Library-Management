using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DAL.Models
{
    public class LibraryManagementDbContextFactory : IDesignTimeDbContextFactory<LibraryManagementDbContext>
    {
        public LibraryManagementDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryManagementDbContext>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).FullName) // Move one level up to Web API directory
                .AddJsonFile("WEB API/appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new LibraryManagementDbContext(optionsBuilder.Options);
        }
    }
}
