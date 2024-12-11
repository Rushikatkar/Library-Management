using DAL.Models.DTOs;
using DAL.Models.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<AuthorDTO>> GetAllAuthorsAsync()
        {
            var authors = await _authorRepository.GetAllAuthorsAsync();
            return authors.Select(a => new AuthorDTO
            {
                Id = a.Id,
                Name = a.Name,
                Biography = a.Biography
            });
        }

        public async Task<AuthorDTO> GetAuthorByIdAsync(int id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author == null) return null;

            return new AuthorDTO
            {
                Id = author.Id,
                Name = author.Name,
                Biography = author.Biography
            };
        }

        public async Task AddAuthorAsync(AuthorCreateDTO authorDTO)
        {
            var author = new Author
            {
                Name = authorDTO.Name,
                Biography = authorDTO.Biography
            };
            await _authorRepository.AddAuthorAsync(author);
        }

        public async Task UpdateAuthorAsync(int id, AuthorUpdateDTO authorDTO)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            if (author != null)
            {
                author.Name = authorDTO.Name;
                author.Biography = authorDTO.Biography;

                await _authorRepository.UpdateAuthorAsync(author);
            }
        }

        public async Task DeleteAuthorAsync(int id)
        {
            await _authorRepository.DeleteAuthorAsync(id);
        }
    }
}
