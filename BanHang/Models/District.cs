namespace BanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("District")]
    public partial class District
    {
        [Key]
        [StringLength(50)]
        public string IdDistrict { get; set; }

        [Required]
        [StringLength(50)]
        public string IdCity { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }
    }
}
