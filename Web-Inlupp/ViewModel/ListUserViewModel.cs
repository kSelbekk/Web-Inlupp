using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Web_Inlupp.ViewModel
{
    public class ListUserViewModel
    {
        public List<ListUser> Users { get; set; } = new List<ListUser>();

        public class ListUser
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
        }
    }
}