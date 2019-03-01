using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ListItemService : IListItemService
    {
        private readonly IRepository _repository;

        public ListItemService(IRepository repository)
        {
            _repository = repository;
        }

        public ListItem AddRecipeToShopList(int recipeId, int shopListId)
        {
            var listItem = _repository.ListAll<ListItem>()
                .Where(li => li.RecipeId == recipeId && li.ShopListId == shopListId)
                .FirstOrDefault();

            if(listItem == null)
            {
                ListItem listItemToBeAdded = new ListItem
                {
                    RecipeId = recipeId,
                    ShopListId = shopListId
                };

                _repository.Add(listItemToBeAdded);
            }
            
            return listItem;
        }

        public void RemoveRecipeFromShopList(int recipeId, int shopListId)
        {
            var listItem = _repository.ListAll<ListItem>()
                .Where(li => li.RecipeId == recipeId && li.ShopListId == shopListId)
                .FirstOrDefault();

            if (listItem != null)
            {
                _repository.Delete(listItem);

            }

        }

        
    }
}
