using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudApi.Models
{
    [Table("Item")]
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Name cannot exceed 20 characters.")]
        public required string Name { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        public string? Description { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public required string CategoryCode { get; set; }

        public string? UpdateBy { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;

        public string? CreateBy { get; set; }

        [Required]
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    }
}
