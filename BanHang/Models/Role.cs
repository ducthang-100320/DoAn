namespace BanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Role")]
    public partial class Role
    {
        [StringLength(20)]
        public string RoleID { get; set; }

        [Required]
        [StringLength(100)]
        public string RoleName { get; set; }

        [StringLength(300)]
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
