using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs.Attributes;
using Web_Inlupp.Data;
using Web_Inlupp.ViewModel;

namespace Web_Inlupp.Controllers
{
    public class ShopController : BaseController
    {
        public ShopController(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IActionResult ShopIndex(string q)
        {
            var viewModel = new ProductIndexViewModel();

            viewModel.Products = DbContext.Products
                .Include(c => c.Category)
                .Where(r => q == null || r.ProductName.Contains(q) || r.Description.Contains(q))
                .Select(dbProd => new ProductIndexViewModel.ProductViewModel
                {
                    Id = dbProd.Id,
                    Name = dbProd.ProductName,
                    Category = dbProd.Category,
                    Description = dbProd.Description,
                    Price = dbProd.Price
                }).ToList();

            return View(viewModel);
        }

        public IActionResult SearchResult(int id)
        {
            var viewModel = new ProductIndexViewModel
            {
                Products = DbContext.Products
                    .Include(c => c.Category)
                    .Where(r => r.Category.Id == id)
                    .Select(dbProd => new ProductIndexViewModel.ProductViewModel
                    {
                        Id = dbProd.Id,
                        Name = dbProd.ProductName,
                        Category = dbProd.Category,
                        Description = dbProd.Description,
                        Price = dbProd.Price
                    }).ToList()
            };

            return View("ShopIndex", viewModel);
        }

        public IActionResult ProductDetails(int id)
        {
            var viewModel = new ProductDetailsViewModel();
            var dbProduct = DbContext.Products
                .Include(c => c.Category)
                .First(i => i.Id == id);

            viewModel.Id = dbProduct.Id;
            viewModel.Name = dbProduct.ProductName;
            viewModel.Category = dbProduct.Category;
            viewModel.Price = dbProduct.Price;
            viewModel.Description = dbProduct.Description;

            return View(viewModel);
        }
    }
}