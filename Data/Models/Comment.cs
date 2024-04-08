using System.ComponentModel.DataAnnotations;

namespace Project.Data.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string info { get; set; } = string.Empty;

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public int RestaurantId { get; set; }
    }
}
