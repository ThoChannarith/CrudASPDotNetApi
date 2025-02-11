using System.ComponentModel.DataAnnotations;

namespace CrudApi.Dtos
{
    public class CategoryCreateDto
    {
        [Required]
        [StringLength(15, ErrorMessage = "Code cannot exceed 15 characters.")]
        public string Code { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        public string? Description { get; set; }
    }
}
