using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BAL;
using DAL.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing authors in the Library Management System.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly ILogger<AuthorsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorsController"/> class.
        /// </summary>
        /// <param name="authorService">Service for managing authors.</param>
        /// <param name="logger">Logger for tracking actions.</param>
        public AuthorsController(IAuthorService authorService, ILogger<AuthorsController> logger)
        {
            _authorService = authorService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all authors.
        /// </summary>
        [Authorize(Roles = "User,Admin")]
        [HttpGet("GetAllAuthors")]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAllAuthors()
        {
            _logger.LogInformation("Request to retrieve all authors initiated.");
            try
            {
                var authors = await _authorService.GetAllAuthorsAsync();
                if (authors == null)
                {
                    _logger.LogWarning("No authors found.");
                    return NotFound(new { Message = "No authors found." });
                }

                _logger.LogInformation("Successfully retrieved all authors.");
                return Ok(authors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving authors.");
                return StatusCode(500, new { Message = "An error occurred while retrieving authors.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves an author by ID.
        /// </summary>
        [Authorize(Roles = "User,Admin")]
        [HttpGet("GetAuthorById/{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthorById(int id)
        {
            _logger.LogInformation($"Request to retrieve author with ID {id} initiated.");
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid author ID provided.");
                    return BadRequest(new { Message = "Invalid author ID." });
                }

                var author = await _authorService.GetAuthorByIdAsync(id);
                if (author == null)
                {
                    _logger.LogWarning($"Author with ID {id} not found.");
                    return NotFound(new { Message = $"Author with ID {id} not found." });
                }

                _logger.LogInformation($"Successfully retrieved author with ID {id}.");
                return Ok(author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving author with ID {id}.");
                return StatusCode(500, new { Message = "An error occurred while retrieving the author.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Creates a new author.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("CreateAuthor")]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorCreateDTO authorDTO)
        {
            _logger.LogInformation("Request to create a new author initiated.");
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid data provided for creating author.");
                    return BadRequest(new { Message = "Invalid data provided.", Errors = ModelState.Values });
                }

                await _authorService.AddAuthorAsync(authorDTO);
                _logger.LogInformation($"Author '{authorDTO.Name}' created successfully.");
                return CreatedAtAction(nameof(GetAuthorById), new { id = authorDTO.Name }, new { Message = "Author created successfully.", Data = authorDTO });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the author.");
                return StatusCode(500, new { Message = "An error occurred while creating the author.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Updates an existing author.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateAuthor/{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorUpdateDTO authorDTO)
        {
            _logger.LogInformation($"Request to update author with ID {id} initiated.");
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid author ID provided for update.");
                    return BadRequest(new { Message = "Invalid author ID." });
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid data provided for updating author.");
                    return BadRequest(new { Message = "Invalid data provided.", Errors = ModelState.Values });
                }

                var existingAuthor = await _authorService.GetAuthorByIdAsync(id);
                if (existingAuthor == null)
                {
                    _logger.LogWarning($"Author with ID {id} not found.");
                    return NotFound(new { Message = $"Author with ID {id} not found." });
                }

                await _authorService.UpdateAuthorAsync(id, authorDTO);
                _logger.LogInformation($"Author with ID {id} updated successfully.");
                return Ok(new { Message = "Author updated successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating author with ID {id}.");
                return StatusCode(500, new { Message = "An error occurred while updating the author.", Details = ex.Message });
            }
        }

        /// <summary>
        /// Deletes an author by ID.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteAuthor/{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            _logger.LogInformation($"Request to delete author with ID {id} initiated.");
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid author ID provided for deletion.");
                    return BadRequest(new { Message = "Invalid author ID." });
                }

                var existingAuthor = await _authorService.GetAuthorByIdAsync(id);
                if (existingAuthor == null)
                {
                    _logger.LogWarning($"Author with ID {id} not found.");
                    return NotFound(new { Message = $"Author with ID {id} not found." });
                }

                await _authorService.DeleteAuthorAsync(id);
                _logger.LogInformation($"Author with ID {id} deleted successfully.");
                return Ok(new { Message = "Author deleted successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting author with ID {id}.");
                return StatusCode(500, new { Message = "An error occurred while deleting the author.", Details = ex.Message });
            }
        }
    }
}
