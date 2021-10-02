namespace BanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductPrice")]
    public partial class ProductPrice
    {
        public int ID { get; set; }

        public int ProductID { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        [StringLength(50)]
        public string Capacity { get; set; }

        public decimal? Price { get; set; }

        public decimal? PromotionPrice { get; set; }

        public DateTime? DateIssued { get; set; }

        public int? IsDate { get; set; }
    }
}
