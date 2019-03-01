
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class ShopListViewModel
    {
        public int ShopListId { get; set; }

        [Required(ErrorMessage = "Du måste fylla i ett namn på inköpslistan")]
        public string Name { get; set; }
        
        public DateTime CreatedDate { get; set; }

        public ICollection<ListItem> ListItems { get; set; }
    }
}
