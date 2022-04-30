using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElevenNote.Models.Category;
using ElevenNote.Services.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElevenNote.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        
        // POST api/Category
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _categoryService.CreateCategoryAsync(request))
                return Ok("Category created successfully.");

            return BadRequest("Category could not be created.");
        }

        // GET api/Category
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        // GET api/Category/5
        [HttpGet("{categoryId:int}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int categoryId)
        {
            var detail = await _categoryService.GetCategoryByIdAsync(categoryId);

            return detail is not null
                ? Ok(detail)
                : NotFound();
        }

        // PUT api/Category
        [HttpPut]
        public async Task<IActionResult> UpdateCategoryById([FromBody] CategoryUpdate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _categoryService.UpdateCategoryAsync(request)
                ? Ok("Category updated successfully.")
                : BadRequest("Category could not be updated.");
        }

        // DELETE api/Category/5
        [HttpDelete("{categoryId:int}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int categoryId)
        {
            return await _categoryService.DeleteCategoryAsync(categoryId)
                ? Ok ($"Category: {categoryId} was deleted successfully.")
                : BadRequest($"Category {categoryId} could not be deleted.");
        }
    }
}
