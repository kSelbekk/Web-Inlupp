using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Inlupp.Data;
using Web_Inlupp.ViewModel;

namespace Web_Inlupp.Controllers
{
    [Authorize(Roles = "Admin, Product Manager")]
    public class ProductManagerController : BaseController
    {
        public ProductManagerController(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        // GET
        public IActionResult ProductEdit(int id)
        {
            var viewModel = new ProductEditViewModel();
            var dbProduct = DbContext.Products
                .Include(c => c.Category)
                .First(i => i.Id == id);

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
            list.AddRange(DbContext.Categories.Select(c => new SelectListItem
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
                var dbProduct = DbContext.Products
                    .Include(c => c.Category)
                    .First(p => p.Id == id);

                dbProduct.ProductName = viewModel.Name;
                dbProduct.Description = viewModel.Description;
                dbProduct.Price = viewModel.Price;
                dbProduct.Category = DbContext.Categories.First(c => c.Id == viewModel.SelectCategoryId);

                DbContext.SaveChanges();
                return RedirectToAction("ShopIndex", "Shop");
            }

            viewModel.AllCategories = GetCategoriesListItems();
            return View(viewModel);
        }

        public IActionResult ProductNew()
        {
            var viewModel = new ProductNewViewModel { AllCategories = GetCategoriesListItems() };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ProductNew(ProductNewViewModel viewModel)
        {
            if (DbContext.Products.Any(c => c.ProductName == viewModel.Name)) ModelState.AddModelError("Name", "Product already exist");
            if (!ModelState.IsValid) return RedirectToAction("Index", "Shop");

            var dbProd = new Product();
            DbContext.Products.Add(dbProd);
            dbProd.Category = DbContext.Categories
                .First(c => c.Id == viewModel.SelectCategoryId);
            dbProd.ProductName = viewModel.Name;
            dbProd.Description = viewModel.Description;
            dbProd.Price = viewModel.Price;
            DbContext.SaveChanges();
            return View(viewModel);
        }

        public IActionResult ProductDelete(int id)
        {
            var dbProd = DbContext.Products.First(p => p.Id.Equals(id));
            DbContext.Products.Remove(dbProd);
            DbContext.SaveChanges();
            return RedirectToAction("Index", "Shop");
        }

        public IActionResult ListCategories()
        {
            var viewModel = new CategoryIndexViewModel
            {
                Categories = DbContext.Categories.Select(dbCat => new CategoryIndexViewModel.CategoryViewModel
                {
                    Id = dbCat.Id,
                    Name = dbCat.CategoryName,
                    Description = dbCat.Description
                }).ToList()
            };

            return View(viewModel);
        }

        public IActionResult EditCategory(int id)
        {
            var viewModel = new EditCategoryViewModel();
            var dbCat = DbContext.Categories.First(i => i.Id == id);

            viewModel.Name = dbCat.CategoryName;
            viewModel.Description = dbCat.Description;
            viewModel.Id = dbCat.Id;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditCategory(EditCategoryViewModel viewModel, int id)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var dbCat = DbContext.Categories
                .First(i => i.Id == id);
            dbCat.CategoryName = viewModel.Name;
            dbCat.Description = viewModel.Description;
            DbContext.SaveChanges();

            return RedirectToAction("ListCategories");
        }

        public IActionResult NewCategory()
        {
            var viewModel = new NewCategoryIndexViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult NewCategory(NewCategoryIndexViewModel viewModel)
        {
            if (DbContext.Categories.Any(c => c.CategoryName == viewModel.Name)) ModelState.AddModelError("Name", "Category already exist");

            if (!ModelState.IsValid) return View(viewModel);

            var dbCat = new Category();
            DbContext.Add(dbCat);
            dbCat.CategoryName = viewModel.Name;
            dbCat.Description = viewModel.Description;
            DbContext.SaveChanges();

            return RedirectToAction("ListCategories");
        }
    }
}