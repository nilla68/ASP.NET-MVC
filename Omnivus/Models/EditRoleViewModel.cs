using System.ComponentModel.DataAnnotations;

namespace Omnivus.Models
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Minimum 2 characters required")]
        public string RoleName { get; set; }
    }
}
