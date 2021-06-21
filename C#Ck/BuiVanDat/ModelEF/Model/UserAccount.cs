namespace ModelEF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserAccount")]
    public partial class UserAccount
    {
        public long ID { get; set; }

        [StringLength(50)]
        [Display(Name ="Tài khoản")]
        public string Username { get; set; }

        [StringLength(50)]
        [Display(Name = "Mật khẩu")] 
        public string Password { get; set; }

        [StringLength(40)]
        [Display(Name = "Trạng thái")] 
        public string Status { get; set; }
        public object CreatedDate { get; internal set; }
        public object CreatedID { get; internal set; }
    }
}
