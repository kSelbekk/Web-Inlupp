using System.Collections.Generic;
using System.Linq;
using System.Web;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartBreadcrumbs.Attributes;
using Web_Inlupp.Data;
using Web_Inlupp.ViewModel;

namespace Web_Inlupp.Controllers
{
    [BreadCrumb(Title="Shop", UseDefaultRouteUrl = true, Order = 1, IgnoreAjaxRequests = true)]
    public class ShopController : BaseController
    {
        public ShopController(ApplicationDbContext dbContext) : base(dbContext)
        { }

        public IActionResult Index(string q, int? id, string order)
        {
            var viewModel = new ProductIndexViewModel();

            viewModel.SearchOption = q;

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

            viewModel.SortingList = GetSortingList();
            
            if (order == null) return View(viewModel);

            foreach (var selectListItem in viewModel.SortingList)
            {
                if (selectListItem.Value == "a-z" && order == selectListItem.Value)
                {
                    viewModel.Products = viewModel.Products.OrderBy(p => p.Name).ToList();
                    return View(viewModel);
                }
                if (selectListItem.Value == "z-a" && order == selectListItem.Value)
                {
                    viewModel.Products = viewModel.Products.OrderByDescending(p => p.Name).ToList();
                    return View(viewModel);
                }
                if (selectListItem.Value == "lowestPrice" && order == selectListItem.Value)
                {
                    viewModel.Products = viewModel.Products.OrderBy(p => p.Price).ToList();
                    return View(viewModel);
                }
                if (selectListItem.Value == "highestPrice" && order == selectListItem.Value)
                {
                    viewModel.Products = viewModel.Products.OrderByDescending(p => p.Price).ToList();
                    return View(viewModel);
                }
            }

            return View(viewModel);
        }

        private List<SelectListItem> GetSortingList()
        {
            var sortingList = new List<SelectListItem>
            {
                new SelectListItem() { Text = "", Value = null},
                new SelectListItem() { Text = "A-Z", Value = "a-z"},
                new SelectListItem() { Text = "Z-A", Value = "z-a"},
                new SelectListItem() { Text = "Highest Price", Value = "highestPrice"},
                new SelectListItem() { Text = "Lowest Price", Value = "lowestPrice"}
            };
            return sortingList;
        }

        [BreadCrumb(Title = "> Products", Order = 2, IgnoreAjaxRequests = true)]
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