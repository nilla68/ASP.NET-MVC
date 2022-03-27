using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Omnivus.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        [PersonalData]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        [PersonalData]
        public string LastName { get; set; }

        public string? ProfileImage { get; set; }
    }
}
