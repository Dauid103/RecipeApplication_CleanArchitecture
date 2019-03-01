
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public interface IListItemService
    {
        ListItem AddRecipeToShopList(int recipeId, int shopListId);
        void RemoveRecipeFromShopList(int recipeId, int shopListId);

    }
}