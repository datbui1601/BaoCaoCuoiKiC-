using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestUngDung.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời nhập tài khoản")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mời nhập mật khẩu")]
        public string Password { get; set; }
        public bool RenameberMe { get; set; }
    }
}