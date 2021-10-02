namespace BanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("City")]
    public partial class City
    {
        [Key]
        [StringLength(50)]
        public string IdCity { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }
    }
}
