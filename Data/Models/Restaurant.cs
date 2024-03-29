using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Project.Data.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]

        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]

        public string Address { get; set; } = string.Empty;

        [Required]
        public string OwnerId { get; set; } = string.Empty;

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;
    }
}
