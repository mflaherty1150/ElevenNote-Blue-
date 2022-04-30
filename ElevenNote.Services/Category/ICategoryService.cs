using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElevenNote.Models.Category;

namespace ElevenNote.Services.Category
{
    public interface ICategoryService
    {
        Task<bool> CreateCategoryAsync(CategoryCreate request);
        Task<IEnumerable<CategoryListItem>> GetAllCategoriesAsync();
        Task<CategoryDetail> GetCategoryByIdAsync(int categoryId);
        Task<bool> UpdateCategoryAsync(CategoryUpdate request);
        Task<bool> DeleteCategoryAsync(int categoryId);
    }
}