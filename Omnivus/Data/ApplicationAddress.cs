using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Omnivus.Data
{
    public class ApplicationAddress
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [PersonalData]
        public string Street { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(10)]
        [PersonalData]
        public string PostCode { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(25)]
        [PersonalData]
        public string City { get; set; }
    }
}
