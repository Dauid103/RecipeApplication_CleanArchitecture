
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class RecipeViewModel
    {
        public int RecipeId { get; set; }
        [Required(ErrorMessage = "Du måste fylla i ett namn på receptet")]
        [Display(Name = "Namn")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Du måste lägga till en bild")]
        [Display(Name = "Bildnamn")]
        public string PictureId { get; set; }
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }
        [Display(Name = "Pris")]
        public int Price { get; set; }

        [Display(Name = "Kategori")]
        public Category RecipeCategory { get; set; }

        public int ShopListId { get; set; }
    }
}
