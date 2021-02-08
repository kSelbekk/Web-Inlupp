using System.ComponentModel.DataAnnotations;

namespace Web_Inlupp.ViewModel
{
    public class NewUserIndexViewModel
    {
        [Required, MaxLength(50)]
        public string UserName { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [Required, Compare("CheckPassword")]
        public string Password { get; set; }

        [Required]
        public string CheckPassword { get; set; }
    }
}