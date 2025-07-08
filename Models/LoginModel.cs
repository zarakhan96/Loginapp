using System.ComponentModel.DataAnnotations;

namespace Loginapp.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string OTP { get; set; }
        [Required(ErrorMessage = "Please select your designation")]
        public string Designation { get; set; }
    }

}
