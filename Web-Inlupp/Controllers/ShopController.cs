using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult ShopIndex(string q, int? id, string order)
        {
            var viewModel = new ProductIndexViewModel();

            viewModel.Products = DbContext.Products
                .Include(c => c.Category)
                .Where(r => q == null && id == null || r.ProductName.Contains(q) || r.Description.Contains(q) || r.Category.Id == id)
                .Select(dbProd => new ProductIndexViewModel.ProductViewModel
                {
                    Id = dbProd.Id,
                    Name = dbProd.ProductName,
                    Category = dbProd.Category,
                    Description = dbProd.Description,
                    Price = dbProd.Price
                }).ToList();

            viewModel.SortingList = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Alphapitcally", Value = "alphapitcally"},
                new SelectListItem() { Text = "Highest Price", Value = "highestPrice"},
                new SelectListItem() { Text = "Lowest Price", Value = "lowestPrice"}
            };

            if (order == null) return View(viewModel);

            foreach (var selectListItem in viewModel.SortingList)
            {
                if (selectListItem.Value == "highestPrice" && order == selectListItem.Value)
                {
                    viewModel.Products = viewModel.Products.OrderByDescending(p => p.Price).ToList();
                    return View(viewModel);
                }

                if (selectListItem.Value == "alphapitcally" && order == selectListItem.Value)
                {
                    viewModel.Products = viewModel.Products.OrderBy(p => p.Name).ToList();
                    return View(viewModel);
                }
                if (selectListItem.Value == "lowestPrice" && order == selectListItem.Value)
                {
                    viewModel.Products = viewModel.Products.OrderBy(p => p.Price).ToList();
                    return View(viewModel);
                }
            }

            return View(viewModel);
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