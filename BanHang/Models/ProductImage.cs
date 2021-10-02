namespace BanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductImage")]
    public partial class ProductImage
    {
        public int ID { get; set; }

        public int? ProductID { get; set; }

        [StringLength(250)]
        public string ImageUrl { get; set; }

        public int? Status { get; set; }

        public int? OrderNumber { get; set; }
    }
}
