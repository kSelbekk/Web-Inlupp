using System.Collections.Generic;
using System.Linq;
using System.Web;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Inlupp.Data;
using Web_Inlupp.ViewModel;

namespace Web_Inlupp.Controllers
{
    [BreadCrumb(Title = "Shop", UseDefaultRouteUrl = true, Order = 1, IgnoreAjaxRequests = true)]
    public class ShopController : BaseController
    {
        public ShopController(ApplicationDbContext dbContext) : base(dbContext)
        { }

        public IActionResult Index(string q, int? id, string order, ProductIndexViewModel viewModel)
        {
            //TODO: Fixa sök med sortering!!!!!

            if (q != null)
            {
                viewModel.SearchOption = q;
            }

            viewModel.Products = DbContext.Products
                .Include(c => c.Category)

                .Where(product => q == null && id == null && viewModel.SearchOption == null ||
                                  product.ProductName.Contains(q) && id == product.Category.Id ||
                                  product.ProductName.Contains(viewModel.SearchOption) ||
                                  product.Description.Contains(q) && id == product.Category.Id ||
                                  product.Category.Id == id)

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

            viewModel = Order(viewModel, order);

            return View(viewModel);
        }

        public ProductIndexViewModel Order(ProductIndexViewModel viewModel, string order)
        {
            foreach (var selectListItem in viewModel.SortingList)
            {
                if (selectListItem.Value == "a-z" && order == selectListItem.Value)
                {
                    viewModel.Products = viewModel.Products.OrderBy(p => p.Name).ToList();
                    return viewModel;
                }
                if (selectListItem.Value == "z-a" && order == selectListItem.Value)
                {
                    viewModel.Products = viewModel.Products.OrderByDescending(p => p.Name).ToList();
                    return viewModel;
                }
                if (selectListItem.Value == "lowestPrice" && order == selectListItem.Value)
                {
                    viewModel.Products = viewModel.Products.OrderBy(p => p.Price).ToList();
                    return viewModel;
                }
                if (selectListItem.Value == "highestPrice" && order == selectListItem.Value)
                {
                    viewModel.Products = viewModel.Products.OrderByDescending(p => p.Price).ToList();
                    return viewModel;
                }
            }

            return null;
        }

        private List<SelectListItem> GetSortingList()
        {
            var sortingList = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Sorting", Value = null},
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