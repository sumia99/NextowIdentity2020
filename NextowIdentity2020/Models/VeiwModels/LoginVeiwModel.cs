using System.ComponentModel.DataAnnotations;

namespace NextowIdentity2020.Models.VeiwModels
{
    public class LoginVeiwModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Enter Email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
