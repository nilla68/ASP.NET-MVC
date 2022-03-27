using System.ComponentModel.DataAnnotations;

namespace Omnivus.Models
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            UserName = "";
            Password = "";
            ReturnUrl = "/";
        }

        [Required]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}