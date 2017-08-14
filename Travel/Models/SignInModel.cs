using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Travel.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage ="Vui lòng điền tên đăng nhập")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng điền mật khẩu")]
        public string Password { get; set; }
    }
}