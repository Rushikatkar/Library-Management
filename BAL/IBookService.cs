using DAL.Models.DTOs;
using DAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public interface IBookService
    {
        Task<IEnumerable<BookDTO>> GetAllBooksAsync(int pageNumber, int pageSize, string sortBy, bool ascending, string filter);
        Task<BookDTO> GetBookByIdAsync(int id);
        Task AddBookAsync(BookCreateDTO bookDTO);
        Task UpdateBookAsync(int id, BookUpdateDTO bookDTO);
        Task DeleteBookAsync(int id);
        Task<int> CountBooksAsync(string filter); // Ensure this signature matches    }

    }
}
