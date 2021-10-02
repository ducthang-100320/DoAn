namespace BanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Manufacturer")]
    public partial class Manufacturer
    {
        public int ID { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public int? CategoryID { get; set; }

        [StringLength(350)]
        public string Image { get; set; }

        public int? Status { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}
