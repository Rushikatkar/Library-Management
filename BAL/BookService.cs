using AutoMapper;
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
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDTO>> GetAllBooksAsync(int pageNumber = 1, int pageSize = 10, string sortBy = "Title", bool ascending = true, string filter = "")
        {
            var books = await _bookRepository.GetAllBooksAsync(pageNumber, pageSize, sortBy, ascending, filter);
            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }

        public async Task<BookDTO> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            return book == null ? null : _mapper.Map<BookDTO>(book);
        }

        public async Task AddBookAsync(BookCreateDTO bookDTO)
        {
            var book = _mapper.Map<Book>(bookDTO);
            await _bookRepository.AddBookAsync(book);
        }

        public async Task UpdateBookAsync(int id, BookUpdateDTO bookDTO)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book != null)
            {
                _mapper.Map(bookDTO, book);
                await _bookRepository.UpdateBookAsync(book);
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteBookAsync(id);
        }

        public async Task<int> CountBooksAsync(string filter)
        {
            return await _bookRepository.CountBooksAsync(filter); // Implement this method in the repository
        }
    }

}
