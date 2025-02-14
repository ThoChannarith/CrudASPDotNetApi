using CrudApi.Data;
using CrudApi.Dtos;
using CrudApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudApi.Services
{
    public class ItemService
    {
        private readonly ApplicationDbContext dbContext;

        public ItemService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Item AddItem(ItemCreateDto itemDto)
        {
            string normalizedName = itemDto.Name;
            decimal price = itemDto.Price;

            if (dbContext.Item.Any(c => c.Name == normalizedName && c.IsActive)) 
            {
                throw new ArgumentException("Code already exists.");
            }
            if (price <= 0) 
            {
                throw new ArgumentException("Price cannot be zero");
            }

            var item = new Item
            {
                Name = itemDto.Name,
                Description = itemDto.Description,
                Price = itemDto.Price,
                CategoryCode = itemDto.CategoryCode.ToUpper(),
                IsActive = true,
                UpdateDate = DateTime.UtcNow,
                CreateDate = DateTime.UtcNow,
            };
            dbContext.Item.Add(item);
            dbContext.SaveChanges();
            return item;
        }

        public IEnumerable<Item> GetAllItem()
        {
            return dbContext.Item.Where(c => c.IsActive).ToList();
        }

        public Item GetItemById(int id)
        {
            var item = dbContext.Item.FirstOrDefault(c => c.Id == id && c.IsActive);

            if (item == null) 
            {
                throw new KeyNotFoundException("Item not found.");
            }
            return item;
        }

        public Item UpdateItem(int id, ItemUpdateDto itemUpdateDto)
        {
            var item = dbContext.Item.FirstOrDefault(c => c.Id == id && c.IsActive);

            if (item == null)
            {
                throw new KeyNotFoundException("Item not found.");
            }

            if (string.IsNullOrWhiteSpace(itemUpdateDto.Name))
            {
                throw new ArgumentException("Name is required.");
            }

            if (string.IsNullOrWhiteSpace(itemUpdateDto.CategoryCode))
            {
                throw new ArgumentException("Category code is required.");
            }

            if (itemUpdateDto.Price <= 0)
            {
                throw new ArgumentException("Price must be greater than zero.");
            }

            // Update item properties
            item.Name = itemUpdateDto.Name;
            item.Description = itemUpdateDto.Description ?? item.Description; // Keep existing description if null
            item.Price = itemUpdateDto.Price;
            item.CategoryCode = itemUpdateDto.CategoryCode.ToUpper();
            item.UpdateDate = DateTime.UtcNow;

            dbContext.SaveChanges();

            return item;
        }

    }
}
