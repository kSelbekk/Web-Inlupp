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

        [Required, DataType(DataType.Password), Compare("CheckPassword")]
        [RegularExpression("^((?=.*[a-z])(?=.*[A-Z])(?=.*\\d)).+$", ErrorMessage = "You need a better password!")]
        public string Password { get; set; }

        [RegularExpression("^((?=.*[a-z])(?=.*[A-Z])(?=.*\\d)).+$")]
        [Required, DataType(DataType.Password)]
        public string CheckPassword { get; set; }
    }
}