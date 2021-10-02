namespace BanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public int ManufacturerID { get; set; }

        public int? MenuID { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(350)]
        public string Image { get; set; }

        public int? Installment { get; set; }

        [StringLength(500)]
        public string AccompanyingProducts { get; set; }

        public int? Warranty { get; set; }

        public int? Status { get; set; }

        public string Description { get; set; }

        public int? ShowHome { get; set; }

        public int? IsHot { get; set; }

        public DateTime? TopHot { get; set; }

        [StringLength(150)]
        public string MetaKeywords { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedUser { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedUser { get; set; }
    }
}
