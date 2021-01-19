using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

namespace Web_Inlupp.Data
{
    public class DataInitializer
    {
        public static void SeedData(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            dbContext.Database.Migrate();
            SeedCategory(dbContext);
        }

        private static void SeedCategory(ApplicationDbContext dbContext)
        {
            var engine = dbContext.Categories.FirstOrDefault(c => c.CategoryName == "Engine");
            if (engine == null)
            {
                dbContext.Categories.Add(new Category()
                {
                    CategoryName = "Engine",
                    Description = "Everything you need for you'r engine"
                });
            }

            var exhaust = dbContext.Categories.FirstOrDefault(c => c.CategoryName == "Exhaust");
            if (exhaust == null)
            {
                dbContext.Categories.Add(new Category()
                {
                    CategoryName = "Exhaust",
                    Description = "You'r car will not sound the same"
                });
            }

            var electric = dbContext.Categories.FirstOrDefault(c => c.CategoryName == "Electric");
            if (electric == null)
            {
                dbContext.Categories.Add(new Category()
                {
                    CategoryName = "Electric",
                    Description = "Make you'r car go faster with our mapping"
                });
            }

            dbContext.SaveChanges();

        }

    }
}