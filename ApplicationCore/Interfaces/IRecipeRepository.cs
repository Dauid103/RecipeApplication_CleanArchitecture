using ApplicationCore.Entities;
using System.Collections.Generic;


namespace ApplicationCore.Interfaces
{
    public interface IRecipeRepository : IRepository
    {
        IEnumerable<Recipe> GetAllRecipesWithCategoryAndShopList();
        Recipe GetRecipeWithCategoryAndShopList(int id);

    }
}