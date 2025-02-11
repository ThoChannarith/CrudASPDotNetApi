using System.ComponentModel.DataAnnotations;

namespace CrudApi.Dtos
{
    public class CategoryUpdateDto
    {
 

        [Required]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string? Name { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        public string? Description { get; set; }

        //public string? UpdateBy { get; set; }

        //[Required]
        //public DateTime UpdateDate { get; set; } = DateTime.UtcNow;
    }
}
