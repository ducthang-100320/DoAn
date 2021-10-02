namespace BanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_Role
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        [Required]
        [StringLength(20)]
        public string RoleID { get; set; }
    }
}
