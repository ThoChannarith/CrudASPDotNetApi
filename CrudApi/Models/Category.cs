using System.ComponentModel.DataAnnotations;

using IndexAttribute = Microsoft.EntityFrameworkCore.IndexAttribute;

namespace CrudApi.Models
{
    [Index(nameof(Code), IsUnique = true)] // Apply the unique index for Code
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Code cannot exceed 15 characters.")]
        public required string Code { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public required string Name { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public string? UpdateBy { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;

        public string? CreateBy { get; set; }

        [Required]
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

    }
}
