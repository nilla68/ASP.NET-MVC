using System.ComponentModel.DataAnnotations;

namespace Omnivus.Models
{
    public class ProfileViewModel
    {
        public ProfileViewModel()
        {
            ReturnUrl = "/";
        }

        [Required]
        [MinLength(2, ErrorMessage = "Minimum 2 characters required")]
        [MaxLength(25, ErrorMessage = "Maximum 25 characters exceeded")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Minimum 2 characters required")]
        [MaxLength(25, ErrorMessage = "Maximum 25 characters exceeded")]
        public string LastName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Minimum 2 characters required")]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters exceeded")]
        public string Street { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Minimum 5 characters required")]
        [MaxLength(10, ErrorMessage = "Maximum 10 characters exceeded")]
        public string PostCode { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Minimum 2 characters required")]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters exceeded")]
        public string City { get; set; }
        
        public string Role { get; set; }
        
        public string? ProfileImageUrl { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Upload File")]
        public IFormFile? File { get; set; }
    }
}
