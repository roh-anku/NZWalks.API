using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTOs
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "minimum 3 characters required")]
        [MaxLength(3, ErrorMessage = "maximum 3 characters required")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100,ErrorMessage ="max 100 chars allowed")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
