using AutoMapper;
using DAL.Models.DTOs;
using DAL.Models.Entities;
using DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task AddCategoryAsync(CategoryCreateDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task UpdateCategoryAsync(int id, CategoryUpdateDTO categoryDTO)
        {
            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);
            if (existingCategory != null)
            {
                _mapper.Map(categoryDTO, existingCategory);
                await _categoryRepository.UpdateCategoryAsync(existingCategory);
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category != null)
            {
                await _categoryRepository.DeleteCategoryAsync(category);
            }
        }
    }
}
