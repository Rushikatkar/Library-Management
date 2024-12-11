using DAL.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(CategoryCreateDTO categoryDTO);
        Task UpdateCategoryAsync(int id, CategoryUpdateDTO categoryDTO);
        Task DeleteCategoryAsync(int id);
    }
}
