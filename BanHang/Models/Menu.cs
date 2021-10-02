namespace BanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        public int ID { get; set; }

        public int ParentID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int? NewsID { get; set; }

        public int? CategoryID { get; set; }

        public int? OrderNumber { get; set; }

        [StringLength(50)]
        public string Icon { get; set; }

        [StringLength(500)]
        public string URL { get; set; }

        public int? Level { get; set; }

        public int? Status { get; set; }
    }
}
