using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ElevenNote.Data;
using ElevenNote.Data.Entities;
using ElevenNote.Models.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ElevenNote.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly int _userId;
        private readonly ApplicationDbContext _dbContext;
        public CategoryService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            var userClaims = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var value = userClaims.FindFirst("id")?.Value;
            var validId = int.TryParse(value, out _userId);
            if (!validId)
                throw new Exception("Attempted to build CategoryService without User Id claim.");

            _dbContext = dbContext;
        }

        public async Task<bool> CreateCategoryAsync(CategoryCreate request)
        {
            var categoryEntity = new CategoryEntity
            {
                CategoryName = request.CategoryName,
                Description = request.Description,
                OwnerId = _userId
            };

            _dbContext.Categories.Add(categoryEntity);
            
            var numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }
        public async Task<IEnumerable<CategoryListItem>> GetAllCategoriesAsync()
        {
            var categories = await _dbContext.Categories
                .Where(entity => entity.OwnerId == _userId)
                .Select(entity => new CategoryListItem
                {
                    CategoryId = entity.CategoryId,
                    CategoryName = entity.CategoryName
                })
                .ToListAsync();

            return categories;
        }
        public async Task<CategoryDetail> GetCategoryByIdAsync(int categoryId)
        {
            // Find the first category that has the given Id and an OwnerId that matches the requesting userId
            var categoryEntity = await _dbContext.Categories
                .FirstOrDefaultAsync(e =>
                    e.CategoryId == categoryId && e.OwnerId == _userId
                );
            
            // If categoryEntity is null then return null, otherwise initialize and return a new CategoryDetail
            return categoryEntity is null ? null : new CategoryDetail
            {
                CategoryId = categoryEntity.CategoryId,
                OwnerId = _userId,
                CategoryName = categoryEntity.CategoryName,
                Description = categoryEntity.Description
            };
        }
        public async Task<bool> UpdateCategoryAsync(CategoryUpdate request)
        {
            var categoryEntity = await _dbContext.Categories.FindAsync(request.CategoryId);

            if (categoryEntity?.OwnerId != _userId)
                return false;

            categoryEntity.CategoryName = request.CategoryName;
            categoryEntity.Description = request.Description;

            var numberOfChanges = await _dbContext.SaveChangesAsync();

            return numberOfChanges == 1;
        }
        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var categoryEntity = await _dbContext.Categories.FindAsync(categoryId);

            if (categoryEntity?.OwnerId != _userId)
                return false;

            _dbContext.Categories.Remove(categoryEntity);
            return await _dbContext.SaveChangesAsync() == 1;
        }
    }
}