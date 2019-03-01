
using ApplicationCore.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RecipeSeeder
    {
        private readonly RecipeContext _context;
        private readonly UserManager<RecipeUser> _userManager;

        public RecipeSeeder(RecipeContext context, UserManager<RecipeUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public async Task SeedDataAsync()
        {
            

            RecipeUser user = await _userManager.FindByEmailAsync("daviande@kth.se");
            if (user == null)
            {
                user = new RecipeUser()
                {
                    LastName = "Andersson",
                    FirstName = "David",
                    Email = "daviande@kth.se",
                    UserName = "daviande@kth.se"
                };

                var result = await _userManager.CreateAsync(user, "Welc0me!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create user in Seeding");
                }
            }

            if (_context.Categories.Any())
            {
                return;
            }

            var categories = new Category[]
            {
                new Category { Name = "Kött"},
                new Category { Name = "Fisk"},
                new Category { Name = "Fågel"},
                new Category { Name = "Vegetariskt"}
            };

            foreach (Category c in categories)
            {
                _context.Categories.Add(c);
            }
            _context.SaveChanges();
            
        }
    }
}
