
using ApplicationCore.Entities;
using System.Collections.Generic;

namespace ApplicationCore.Interfaces
{
    public interface IShopListRepository : IRepository
    {
        IEnumerable<ShopList> GetAllShopListsWithRecipes();
        ShopList GetShopListWithRecipes(int id);
    }
}