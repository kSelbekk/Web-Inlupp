using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Inlupp.Data;
using Web_Inlupp.ViewModel;

namespace Web_Inlupp.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext _dbContext;

        public AdministrationController(RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext)
        {
            this.roleManager = roleManager;
            _dbContext = dbContext;
        }

        public IActionResult ProductEdit(int id)
        {
            var viewModel = new ProductEditViewModel();
            var dbProduct = _dbContext.Products.Include(c => c.Category).First(i => i.Id == id);

            viewModel.Id = dbProduct.Id;
            viewModel.Name = dbProduct.ProductName;
            viewModel.AllCategories = GetCategoriesListItems();
            viewModel.SelectCategoryId = dbProduct.Category.Id;
            viewModel.Price = dbProduct.Price;
            viewModel.Description = dbProduct.Description;

            return View(viewModel);
        }

        private List<SelectListItem> GetCategoriesListItems()
        {
            var list = new List<SelectListItem>();
            list.AddRange(_dbContext.Categories.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.Id.ToString()
            }));
            return list;
        }

        [HttpPost]
        public IActionResult ProductEdit(int id, ProductEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var dbProduct = _dbContext.Products
                    .Include(c => c.Category)
                    .First(p => p.Id == id);

                dbProduct.ProductName = viewModel.Name;
                dbProduct.Description = viewModel.Description;
                dbProduct.Price = viewModel.Price;
                dbProduct.Category = _dbContext.Categories.First(c => c.Id == viewModel.SelectCategoryId);

                _dbContext.SaveChanges();
            }

            viewModel.AllCategories = GetCategoriesListItems();
            return View(viewModel);
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectRoleViewModel role)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = role.RoleName
                };

                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "Home");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }
    }
}