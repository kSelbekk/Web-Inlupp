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
            SeedRoles(dbContext);
            SeedUser(userManager);
        }

        private static void SeedRoles(ApplicationDbContext dbContext)
        {
            var role = dbContext.Roles.FirstOrDefault(a => a.Name == "Admin");
            if (role == null)
            {
                dbContext.Roles.Add(new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper() });
            }
            role = dbContext.Roles.FirstOrDefault(a => a.Name == "Product Manager");
            if (role == null)
            {
                dbContext.Roles.Add(new IdentityRole { Name = "Product Manager", NormalizedName = "Product Manager".ToUpper() });
            }

            dbContext.SaveChanges();
        }

        private static void SeedUser(UserManager<IdentityUser> userManager)
        {
            AddRoleIfNotExists(userManager, "stefan.holmberg@systementor.se", "Hejsan123#", new[] { "Admin" });
            AddRoleIfNotExists(userManager, "stefan.holmbergmanager@systementor.se", "Hejsan123#", new[] { "Product Manager" });
        }

        private static void AddRoleIfNotExists(UserManager<IdentityUser> userManager, string userName, string password, string[] role)
        {
            if (userManager.FindByEmailAsync(userName).Result != null)
                return;
            var identityUser = new IdentityUser
            {
                UserName = userName,
                Email = userName,
                EmailConfirmed = true
            };
            var result = userManager.CreateAsync(identityUser, password).Result;

            var r = userManager.AddToRolesAsync(identityUser, role).Result;
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
                    Price = 10000m
                });
            }

            var pistonRings = dbContext.Products
                .FirstOrDefault(p => p.ProductName == "Piston rings");
            if (pistonRings == null)
            {
                dbContext.Products.Add(new Product()
                {
                    ProductName = "Piston rings",
                    Description = "For evert enging ever",
                    Category = dbContext.Categories.First(c => c.CategoryName == "Engine"),
                    Price = 10000m
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
                    Price = 35000m
                });
            }

            var milltekSport = dbContext.Products
                .FirstOrDefault(p => p.ProductName == "Milltek Sport");
            if (milltekSport == null)
            {
                dbContext.Products.Add(new Product()
                {
                    ProductName = "Milltek Sport",
                    Description = "The best exhaust on the market",
                    Category = dbContext.Categories.First(c => c.CategoryName == "Exhaust"),
                    Price = 38000m
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
                    Price = 14999m
                });
            }

            var spark = dbContext.Products
                .FirstOrDefault(p => p.ProductName == "Sparkplugs");
            if (spark == null)
            {
                dbContext.Products.Add(new Product()
                {
                    ProductName = "Sparkplugs",
                    Description = "For that perfect ignition timing",
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

        private void AddRoleIfNotExists(ApplicationDbContext context, string role)
        {
            if (context.Roles.Any(r => r.Name == role)) return;
            context.Roles.Add(new IdentityRole { Name = role, NormalizedName = role });
            context.SaveChanges();
        }
    }
}