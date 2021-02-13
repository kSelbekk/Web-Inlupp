using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Inlupp.Data;
using Web_Inlupp.ViewModel;

namespace Web_Inlupp.Controllers
{
    public class ShoppingCartController : BaseController
    {
        public ShoppingCartController(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        // GET
        public IActionResult Index()
        {
            var viewModel = new ShoppingCartIndexViewModel
            {
                ShoppingCartViewModels = DbContext.ShoppingCart.Select(dbCat => new ShoppingCartIndexViewModel.ShoppingCartViewModel
                {
                    Id = dbCat.Id,
                    Price = dbCat.Price,
                }).ToList()
            };

            return View(viewModel);
        }

        public IActionResult AddToCart(int id)
        {
            var cartProduct = new ShoppingCart();
            var dbProd = DbContext.Products.FirstOrDefault(i => i.Id == id);

            cartProduct.Id = dbProd.Id;
            cartProduct.Product = dbProd;
            cartProduct.Price = dbProd.Price;
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}