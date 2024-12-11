using DAL.Models.Entities;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryManagementDbContext _context;

        public BookRepository(LibraryManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(int pageNumber = 1, int pageSize = 10, string sortBy = "Title", bool ascending = true, string filter = "")
        {
            IQueryable<Book> query = _context.Books
                                             .Include(b => b.Author)
                                             .Include(b => b.Category);

            // Filtering
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(b => b.Title.Contains(filter) || b.Author.Name.Contains(filter) || b.Category.Name.Contains(filter));
            }

            // Sorting
            if (ascending)
            {
                query = query.OrderBy(b => EF.Property<object>(b, sortBy));
            }
            else
            {
                query = query.OrderByDescending(b => EF.Property<object>(b, sortBy));
            }

            // Pagination
            return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }


        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books
                                 .Include(b => b.Author)
                                 .Include(b => b.Category)
                                 .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> CountBooksAsync(string filter)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(b => b.Title.Contains(filter) || b.Author.Name.Contains(filter)); // Example filter logic
            }

            return await query.CountAsync();
        }
    }

}
