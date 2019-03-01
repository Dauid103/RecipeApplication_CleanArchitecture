using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class ShopList
    {
        public int ShopListId { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<ListItem> ListItems { get; set; }
    }
}
