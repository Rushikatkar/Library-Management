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
    /// Controller for managing categories.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>A list of categories.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllCategories")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories()
        {
            _logger.LogInformation("Starting retrieval of all categories.");
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving categories.");
                return StatusCode(500, new { Message = "An error occurred while retrieving categories. Please try again later." });
            }
        }

        /// <summary>
        /// Retrieves a category by ID.
        /// </summary>
        /// <param name="id">The category ID.</param>
        /// <returns>The category with the specified ID.</returns>
        [Authorize(Roles = "User,Admin")]
        [HttpGet("GetCategoryById/{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            _logger.LogInformation("Starting retrieval of category with ID {Id}.", id);
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid category ID provided: {Id}.", id);
                    return BadRequest(new { Message = "Invalid category ID provided." });
                }

                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    _logger.LogWarning("Category with ID {Id} not found.", id);
                    return NotFound(new { Message = $"Category with ID {id} not found." });
                }

                _logger.LogInformation("Successfully retrieved category with ID {Id}.", id);
                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the category with ID {Id}.", id);
                return StatusCode(500, new { Message = "An error occurred while retrieving the category. Please try again later." });
            }
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="categoryDTO">The category details.</param>
        [Authorize(Roles = "Admin")]
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDTO categoryDTO)
        {
            _logger.LogInformation("Starting creation of a new category.");
            try
            {
                if (categoryDTO == null)
                {
                    _logger.LogWarning("Category data provided is null.");
                    return BadRequest(new { Message = "Category data cannot be null." });
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid data provided for creating category.");
                    return BadRequest(new { Message = "Invalid data provided.", Errors = ModelState.Values });
                }

                await _categoryService.AddCategoryAsync(categoryDTO);
                _logger.LogInformation("Successfully created category with name {Name}.", categoryDTO.Name);
                return CreatedAtAction(nameof(GetCategoryById), new { id = categoryDTO.Name }, categoryDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the category.");
                return StatusCode(500, new { Message = "An error occurred while creating the category. Please try again later." });
            }
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="id">The category ID.</param>
        /// <param name="categoryDTO">The updated category details.</param>
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDTO categoryDTO)
        {
            _logger.LogInformation("Starting update for category with ID {Id}.", id);
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid category ID provided for update: {Id}.", id);
                    return BadRequest(new { Message = "Invalid category ID provided." });
                }

                if (categoryDTO == null)
                {
                    _logger.LogWarning("Category data provided for update is null.");
                    return BadRequest(new { Message = "Category data cannot be null." });
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid data provided for updating category with ID {Id}.", id);
                    return BadRequest(new { Message = "Invalid data provided.", Errors = ModelState.Values });
                }

                var existingCategory = await _categoryService.GetCategoryByIdAsync(id);
                if (existingCategory == null)
                {
                    _logger.LogWarning("Category with ID {Id} not found for update.", id);
                    return NotFound(new { Message = $"Category with ID {id} not found." });
                }

                await _categoryService.UpdateCategoryAsync(id, categoryDTO);
                _logger.LogInformation("Successfully updated category with ID {Id}.", id);
                return Ok(new { Message = "Category updated successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the category with ID {Id}.", id);
                return StatusCode(500, new { Message = "An error occurred while updating the category. Please try again later." });
            }
        }

        /// <summary>
        /// Deletes a category by ID.
        /// </summary>
        /// <param name="id">The category ID.</param>
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            _logger.LogInformation("Starting deletion of category with ID {Id}.", id);
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid category ID provided for deletion: {Id}.", id);
                    return BadRequest(new { Message = "Invalid category ID provided." });
                }

                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    _logger.LogWarning("Category with ID {Id} not found for deletion.", id);
                    return NotFound(new { Message = $"Category with ID {id} not found." });
                }

                await _categoryService.DeleteCategoryAsync(id);
                _logger.LogInformation("Successfully deleted category with ID {Id}.", id);
                return Ok(new { Message = "Category deleted successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the category with ID {Id}.", id);
                return StatusCode(500, new { Message = "An error occurred while deleting the category. Please try again later." });
            }
        }
    }
}
