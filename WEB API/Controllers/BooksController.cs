using BAL;
using DAL.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookService bookService, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("GetAllBooks")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAllBooks(
           [FromQuery] int pageNumber = 1,
           [FromQuery] int pageSize = 10,
           [FromQuery] string sortBy = "Title",
           [FromQuery] bool ascending = true,
           [FromQuery] string filter = "")
        {
            _logger.LogInformation("Request to retrieve all books with pagination started. Page: {PageNumber}, Size: {PageSize}, SortBy: {SortBy}, Ascending: {Ascending}, Filter: {Filter}", pageNumber, pageSize, sortBy, ascending, filter);

            try
            {
                var books = await _bookService.GetAllBooksAsync(pageNumber, pageSize, sortBy, ascending, filter);
                var totalBooks = await _bookService.CountBooksAsync(filter);
                var totalPages = (int)Math.Ceiling(totalBooks / (double)pageSize);


                var result = new
                {
                    Data = books,
                    TotalCount = totalBooks,
                    TotalPages = totalPages,
                    CurrentPage = pageNumber
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving books.");
                return StatusCode(500, new { Message = "An error occurred while retrieving books. Please try again later." });
            }
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet("GetBookById/{id}")]
        public async Task<ActionResult<BookDTO>> GetBookById(int id)
        {
            _logger.LogInformation("Request to retrieve book by ID {Id} started.", id);

            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid book ID {Id} provided.", id);
                    return BadRequest(new { Message = "Invalid book ID provided." });
                }

                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null)
                {
                    _logger.LogWarning("Book with ID {Id} not found.", id);
                    return NotFound(new { Message = $"Book with ID {id} not found." });
                }

                _logger.LogInformation("Successfully retrieved book with ID {Id}.", id);
                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the book with ID {Id}.", id);
                return StatusCode(500, new { Message = "An error occurred while retrieving the book. Please try again later." });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateBook")]
        public async Task<IActionResult> CreateBook([FromBody] BookCreateDTO bookDTO)
        {
            _logger.LogInformation("Request to create a new book started.");

            try
            {
                if (bookDTO == null)
                {
                    _logger.LogWarning("Null book data provided for creation.");
                    return BadRequest(new { Message = "Book data cannot be null." });
                }

                await _bookService.AddBookAsync(bookDTO);
                _logger.LogInformation("Book '{Title}' created successfully.", bookDTO.Title);
                return CreatedAtAction(nameof(GetBookById), new { id = bookDTO.Title }, bookDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the book.");
                return StatusCode(500, new { Message = "An error occurred while creating the book. Please try again later." });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateBook/{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookUpdateDTO bookDTO)
        {
            _logger.LogInformation("Request to update book with ID {Id} started.", id);

            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid book ID {Id} provided for update.", id);
                    return BadRequest(new { Message = "Invalid book ID provided." });
                }

                if (bookDTO == null)
                {
                    _logger.LogWarning("Null book data provided for update.");
                    return BadRequest(new { Message = "Book data cannot be null." });
                }

                await _bookService.UpdateBookAsync(id, bookDTO);
                _logger.LogInformation("Book with ID {Id} updated successfully.", id);
                return Ok(new { Message = "Book updated successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the book with ID {Id}.", id);
                return StatusCode(500, new { Message = "An error occurred while updating the book. Please try again later." });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteBook/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            _logger.LogInformation("Request to delete book with ID {Id} started.", id);

            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid book ID {Id} provided for deletion.", id);
                    return BadRequest(new { Message = "Invalid book ID provided." });
                }

                await _bookService.DeleteBookAsync(id);
                _logger.LogInformation("Book with ID {Id} deleted successfully.", id);
                return Ok(new { Message = "Book deleted successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the book with ID {Id}.", id);
                return StatusCode(500, new { Message = "An error occurred while deleting the book. Please try again later." });
            }
        }
    }
}
