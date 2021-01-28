using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web_Inlupp.Data;
using Web_Inlupp.ViewModel;

namespace Web_Inlupp.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ApplicationDbContext DbContext;

        public BaseController(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewData["Categories"] = DbContext.Categories
                .Select(dbCat => new CategoryIndexViewModel.CategoryViewModel
                {
                    Id = dbCat.Id,
                    Name = dbCat.CategoryName
                }).ToList();
            base.OnActionExecuting(context);
        }
    }
}