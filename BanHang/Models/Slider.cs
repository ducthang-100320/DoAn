namespace BanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slider")]
    public partial class Slider
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [StringLength(500)]
        public string LinkURL { get; set; }

        public int? OrderNumber { get; set; }

        public int? Status { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
