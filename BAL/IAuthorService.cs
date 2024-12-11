using DAL.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDTO>> GetAllAuthorsAsync();
        Task<AuthorDTO> GetAuthorByIdAsync(int id);
        Task AddAuthorAsync(AuthorCreateDTO authorDTO);
        Task UpdateAuthorAsync(int id, AuthorUpdateDTO authorDTO);
        Task DeleteAuthorAsync(int id);
    }
}
