using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace D14_ADONET.Models
{
    public class Loai
    {
        [Display(Name ="Mã loại")]
        public int MaLoai { get; set; }
        [Display(Name = "Tên loại")]
        [Required(ErrorMessage ="*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        public string TenLoai { get; set; }
        [Display(Name = "Hình")]
        [MaxLength(50, ErrorMessage ="Tối đa 50 kí tự")]
        public string Hinh { get; set; }
        [Display(Name = "Mô tả")]
        [DataType(DataType.MultilineText)]
        public string MoTa { get; set; }
    }
}
