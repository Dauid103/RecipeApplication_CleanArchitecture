using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string PictureId { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<ListItem> ListItems { get; set; }

    }
}
