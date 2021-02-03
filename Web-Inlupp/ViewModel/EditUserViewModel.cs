using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Web_Inlupp.ViewModel
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [MaxLength(80), Required]
        public string UserName { get; set; }

        [MaxLength(80), Required, EmailAddress]
        public string Email { get; set; }
    }
}