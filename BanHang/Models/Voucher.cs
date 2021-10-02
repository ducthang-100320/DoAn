namespace BanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Voucher")]
    public partial class Voucher
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string VoucherCode { get; set; }

        public int? ProductID { get; set; }

        public decimal? Price { get; set; }

        public decimal? ConditionPrice { get; set; }

        public DateTime? DateIssue { get; set; }

        public int? Status { get; set; }
    }
}
