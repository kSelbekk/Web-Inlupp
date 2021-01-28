using System.Collections.Generic;

namespace Web_Inlupp.ViewModel
{
    public class CategoryIndexViewModel
    {
        public string q { get; set; }
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

        public class CategoryViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }
}