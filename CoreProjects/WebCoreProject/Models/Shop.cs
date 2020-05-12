using System;
using System.Collections.Generic;

namespace WebCoreProject.Models
{
    public partial class Shop
    {
        public int Id { get; set; }
        public string ShopName { get; set; }
        public double ShopPrice { get; set; }
        public bool? States { get; set; }
    }
}
