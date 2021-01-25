﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_Inlupp.ViewModel
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Role Name is required")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; } = new List<string>();
    }
}