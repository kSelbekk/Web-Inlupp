using System.ComponentModel.DataAnnotations;

namespace Web_Inlupp.ViewModel
{
    public class EditUserPassword
    {
        public string Id { get; set; }

        [DataType(DataType.Password)]
        [Compare("PasswordChecked")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        public string PasswordChecked { get; set; }
    }
}