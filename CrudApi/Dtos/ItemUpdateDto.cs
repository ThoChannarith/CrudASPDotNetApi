using System.ComponentModel.DataAnnotations;

namespace CrudApi.Dtos
{
    public class ItemUpdateDto
    {
        [Required]
        [StringLength(20, ErrorMessage = "Name cannot exceed 20 characters.")]
        public required string Name { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        public string? Description { get; set; }

        public decimal Price { get; set; }

        [Required]
        public required string CategoryCode { get; set; }
    }
}
