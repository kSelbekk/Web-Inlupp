using System.ComponentModel.DataAnnotations;

namespace Web_Inlupp.ViewModel
{
    public class ProjectRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}