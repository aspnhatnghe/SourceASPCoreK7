using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace D21_Session.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Mã khách hàng")]
        [Required(ErrorMessage = "*")]
        public string MaKH { get; set; }
        [Display(Name ="Mật khẩu")]
        [Required(ErrorMessage ="*")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }
    }
}
