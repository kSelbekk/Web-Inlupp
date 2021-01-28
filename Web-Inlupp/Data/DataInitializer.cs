using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.DependencyInjection;

namespace Web_Inlupp.Data
{
    public class DataInitializer
    {
        public static void SeedData(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            dbContext.Database.Migrate();
            SeedCategory(dbContext);
            SeedProducts(dbContext);
        }

        private static void SeedProducts(ApplicationDbContext dbContext)
        {
            var piston = dbContext.Products
                .FirstOrDefault(p => p.ProductName == "Piston");
            if (piston == null)
            {
                dbContext.Products.Add(new Product()
                {
                    ProductName = "Piston",
                    Description = "Perfect for cars with turbo",
                    Category = dbContext.Categories.First(c => c.CategoryName == "Engine"),
                    Price = 100m
                });
            }

            var akrapovic = dbContext.Products
                .FirstOrDefault(p => p.ProductName == "Akrapovic");
            if (akrapovic == null)
            {
                dbContext.Products.Add(new Product()
                {
                    ProductName = "Akrapovic",
                    Description = "The best exhaust on the market",
                    Category = dbContext.Categories.First(c => c.CategoryName == "Exhaust"),
                    Price = 3500m
                });
            }

            var bsr = dbContext.Products
                .FirstOrDefault(p => p.ProductName == "Bsr tuning chip");
            if (bsr == null)
            {
                dbContext.Products.Add(new Product()
                {
                    ProductName = "Bsr tuning chip",
                    Description = "Adds 30hp to the car",
                    Category = dbContext.Categories.First(c => c.CategoryName == "Electric"),
                    Price = 3500m
                });
            }

            dbContext.SaveChanges();
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