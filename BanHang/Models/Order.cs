namespace BanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public int ID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UserID { get; set; }

        [StringLength(500)]
        public string OrderAddress { get; set; }

        [StringLength(12)]
        public string OrderPhone { get; set; }

        public int? Status { get; set; }
    }
}
