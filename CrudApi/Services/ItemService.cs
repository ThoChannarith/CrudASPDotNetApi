using CrudApi.Data;
using CrudApi.Dtos;
using CrudApi.Models;

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

            if (dbContext.Item.Any(c => c.Name == normalizedName)) 
            {
            
            }

            var item = new Item
            {
                Name = itemDto.Name,
                Description = itemDto.Description,
                Price = itemDto.Price,
                CategoryCode = itemDto.CategoryCode,
                IsActive = true,
                UpdateDate = DateTime.UtcNow,
                CreateDate = DateTime.UtcNow,
            };
            dbContext.Item.Add(item);
            dbContext.SaveChanges();
            return item;
        }
    }
}
