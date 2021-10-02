namespace BanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public int ID { get; set; }

        public int Account_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string Image { get; set; }

        public int? Sex { get; set; }

        public DateTime? Birthday { get; set; }

        [StringLength(12)]
        public string Phone { get; set; }

        [StringLength(300)]
        public string Address { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedUser { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedUser { get; set; }
    }
}
