using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repos
{
    public class ShopListRepository : Repository, IShopListRepository
    {

        public ShopListRepository(RecipeContext context) : base(context)
        {
        }

        public IEnumerable<ShopList> GetAllShopListsWithRecipes()
        {
            return _context.ShopLists
                .Include(sl => sl.ListItems)
                    .ThenInclude(li => li.Recipe)
                .AsNoTracking()
                .ToList();
        }

        public ShopList GetShopListWithRecipes(int id)
        {
            return _context.ShopLists
                .Include(sl => sl.ListItems)
                    .ThenInclude(li => li.Recipe)
                .Where(sl => sl.ShopListId == id)
                .FirstOrDefault();
        }
    }
}
