using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BLL
{
    public class RegisterMV
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;


        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
