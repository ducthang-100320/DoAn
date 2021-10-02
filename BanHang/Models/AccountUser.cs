using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanHang.Models
{
    public partial class AccountUser
    {
        public Account _Account { get; set; }
        public User _User { get; set; }
    }
}