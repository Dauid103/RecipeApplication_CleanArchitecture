using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class ListItem
    {
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public int ShopListId { get; set; }
        public ShopList ShopList { get; set; }

    }
}
