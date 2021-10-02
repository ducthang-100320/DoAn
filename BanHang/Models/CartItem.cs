using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanHang.Models
{
    public partial class CartItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public decimal PromotionPrice { get; set; }
        public int Quantity { get; set; }
    }
}