namespace BanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FeedBack")]
    public partial class FeedBack
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(12)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(350)]
        public string Address { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public int? Status { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
