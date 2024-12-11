using DAL.Models;
using DAL.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryManagementDbContext _context;

        public AuthorRepository(LibraryManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _context.Set<Author>().ToListAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _context.Set<Author>().FindAsync(id);
        }

        public async Task AddAuthorAsync(Author author)
        {
            await _context.Set<Author>().AddAsync(author);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            _context.Set<Author>().Update(author);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await _context.Set<Author>().FindAsync(id);
            if (author != null)
            {
                _context.Set<Author>().Remove(author);
                await _context.SaveChangesAsync();
            }
        }
    }
}
