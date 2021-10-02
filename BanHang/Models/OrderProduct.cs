using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanHang.Models
{
    public partial class OrderProduct
    {
        public Order _order { get; set; }
        public OrderDetail _orderDetail { get; set; }
    }
}