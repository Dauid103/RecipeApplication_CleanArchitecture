using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repos
{
    public class RecipeRepository : Repository, IRecipeRepository
    {
       
        public RecipeRepository(RecipeContext context) : base(context)
        {
        }

        public IEnumerable<Recipe> GetAllRecipesWithCategoryAndShopList()
        {
            return _context.Recipes
                .Include(r => r.Category)
                .Include(r => r.ListItems)
                    .ThenInclude(li => li.ShopList)
                .AsNoTracking()
                .ToList();
        }

        public Recipe GetRecipeWithCategoryAndShopList(int id)
        {
            return _context.Recipes
                 .Include(r => r.Category)
                 .Include(r => r.ListItems)
                    .ThenInclude(li => li.ShopList)
                 .Where(r => r.RecipeId == id)
                 .SingleOrDefault();

        } 

    }
}
