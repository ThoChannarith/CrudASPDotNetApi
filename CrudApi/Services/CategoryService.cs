using CrudApi.Data;
using CrudApi.Dtos;
using CrudApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudApi.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Category AddCategory(CategoryCreateDto categoryDto)
        {
            if (string.IsNullOrWhiteSpace(categoryDto.Code))
            {
                throw new ArgumentException("Code cannot be null or empty.");
            }

            // Normalize the code to upper case for case-insensitive comparison
            string normalizedCode = categoryDto.Code.ToUpper().Replace(" ", "_");



            bool isCodeExists = dbContext.Category
            .Any(c => c.Code.ToUpper() == normalizedCode && c.IsActive);

            if (isCodeExists)
            {
                throw new ArgumentException("Category code already exists.");
            }
            try
            {
                var category = new Category
                {
                    Code = normalizedCode,
                    Name = categoryDto.Name,  // Ensure no null issues
                    Description = categoryDto.Description?.Trim(),
                    IsActive = true,
                    UpdateDate = DateTime.UtcNow,
                    CreateDate = DateTime.UtcNow,
                };

                dbContext.Category.Add(category);
                dbContext.SaveChanges();  // <-- Error likely happens here

                return category;
            }
            catch (DbUpdateException ex) // Catch DB constraint violations
            {
                throw new Exception("Database error: Possible duplicate code constraint.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while saving the category.", ex);
            }


        }

        public IEnumerable<Category> GetAllCategory()
        {
            return dbContext.Category.Where(c => c.IsActive).ToList();
        }

        public Category GetCategoryByCode(string code)
        {
            var normalizedCode = code.ToUpper().Replace(" ", "_");
            var category = dbContext.Category.FirstOrDefault(c => c.Code == normalizedCode);

            if (category == null)
            {
                throw new KeyNotFoundException($"Category not found.");
            }

            return category;
        }

        public Category UpdateCategory(string code, CategoryUpdateDto updateDto)
        {
            var normalizedCode = code.ToUpper().Replace(" ", "_");
            var category = dbContext.Category.FirstOrDefault(c => c.Code == normalizedCode);

            if (category == null)
            {
                throw new KeyNotFoundException("Category not found.");
            }

            // Update only if fields are provided
            if (!string.IsNullOrWhiteSpace(updateDto.Name))
            {
                category.Name = updateDto.Name;
            }

            if (updateDto.Description != null) // Description can be null
            {
                category.Description = updateDto.Description;
            }

            
            category.UpdateDate = DateTime.UtcNow;

            dbContext.SaveChanges();

            return category;
        }

        public void DeleteCategory(string code)
        {
            // Normalize the code to uppercase and replace spaces with underscores
            var normalizedCode = code.ToUpper().Replace(" ", "_");

            // Look for an active category with the given code
            var category = dbContext.Category.FirstOrDefault(c => c.Code == normalizedCode && c.IsActive);

            if (category == null)
            {
                throw new KeyNotFoundException($"Category Code not found or already deleted.");
            }

            // Mark the record as deleted (inactive)
            category.IsActive = false;
            category.UpdateDate = DateTime.UtcNow;

            // Save changes to the database
            dbContext.SaveChanges();
        }


    }

}
