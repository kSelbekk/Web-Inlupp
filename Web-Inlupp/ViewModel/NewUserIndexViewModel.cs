using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web_Inlupp.ViewModel
{
    public class NewUserIndexViewModel
    {
        [Required, MaxLength(50)]
        public string UserName { get; set; }

        [Required, MaxLength(100), EmailAddress]
        public string Email { get; set; }

        [Required, PasswordPropertyText, Compare("CheckPassword")]
        public string Password { get; set; }

        [Required, PasswordPropertyText]
        public string CheckPassword { get; set; }
    }
}