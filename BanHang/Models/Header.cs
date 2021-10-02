namespace BanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Header")]
    public partial class Header
    {
        public int ID { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public int? Status { get; set; }

        [StringLength(150)]
        public string Address { get; set; }
    }
}
