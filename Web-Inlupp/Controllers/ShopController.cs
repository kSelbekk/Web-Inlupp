using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Inlupp.Data;
using Web_Inlupp.ViewModel;

namespace Web_Inlupp.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        // GET
        public IActionResult ShopIndex2()
        {
            return View();
        }

        public IActionResult ShopIndex()
        {
            var viewModel = new ProductIndexViewModel();
            viewModel.products = _dbContext.Products
                .Select(dbProd => new ProductIndexViewModel.ProductViewModel()
                {
                    Id = dbProd.Id,
                    Name = dbProd.ProductName,
                    Category = dbProd.Category,
                    Description = dbProd.Description,
                    Price = dbProd.Price
                }).ToList();

            return View(viewModel);
        }

        public IActionResult ProductDetails(int id)
        {
            var viewModel = new ProductDetailsViewModel();
            var dbProduct = _dbContext.Products.First(i => i.Id == id);

            viewModel.Id = dbProduct.Id;
            viewModel.Name = dbProduct.ProductName;
            viewModel.Price = dbProduct.Price;
            viewModel.Description = dbProduct.Description;

            return View(viewModel);
        }

        public ShopController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}