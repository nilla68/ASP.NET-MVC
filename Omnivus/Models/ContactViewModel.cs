using System.ComponentModel.DataAnnotations;

namespace Omnivus.Models
{
    public class ContactViewModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "Minimum 2 characters required")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Minimum 5 characters required")]
        [MaxLength(300, ErrorMessage = "Maximum 300 characters exceeded")]
        public string Message { get; set; }
    }
}