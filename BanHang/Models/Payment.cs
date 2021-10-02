namespace BanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Payment")]
    public partial class Payment
    {
        public int ID { get; set; }

        public int? OrderDetailID { get; set; }

        public int? BankID { get; set; }

        public int Status { get; set; }
    }
}
