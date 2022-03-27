using System.ComponentModel.DataAnnotations;

namespace Omnivus.Models
{
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {
            FirstName = "";
            LastName = "";
            Email = "";
            Password = "";
            ReturnUrl = "/";
            RoleName = "user";
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
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

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

        public string ReturnUrl { get; set; }
        public string RoleName { get; set; }

        public string? Error { get; set; }
    }
}