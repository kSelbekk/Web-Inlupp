using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal;

namespace Web_Inlupp.ViewModel
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [MaxLength(100), Required]
        public string UserName { get; set; }

        [MaxLength(100), Required, EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password), Required]
        [Compare("PasswordChecked")]
        public string Password { get; set; }

        [DataType(DataType.Password), Required]
        public string PasswordChecked { get; set; }
    }
}