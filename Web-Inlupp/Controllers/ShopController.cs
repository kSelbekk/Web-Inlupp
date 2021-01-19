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
            var viewModel = new CategoryIndexViewModel();
            viewModel.Categories = _dbContext.Categories
                .Select(dbCat => new CategoryIndexViewModel.CategoryViewModel()
                {
                    Id = dbCat.Id,
                    Name = dbCat.CategoryName,
                    Description = dbCat.Description
                }).ToList();

            return View(viewModel);
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

        public IActionResult Edit(int Id)
        {
            return View();
        }

        public IActionResult New()
        {
            return View();
        }

        public ShopController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}