using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace D19_EFCore_CodeFirst.Models
{
    [Table("Loai")]
    public class Loai
    {
        [Key]
        [Display(Name ="Mã loại")]
        public int MaLoai { get; set; }
        [Required(ErrorMessage = "Chưa nhập tên hàng hóa")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        [Display(Name = "Tên loại")]
        public string TenLoai { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
        [MaxLength(200, ErrorMessage = "Tối đa 200 kí tự")]
        [Display(Name = "Hình")]
        public string Hinh { get; set; }

        public ICollection<HangHoa> HangHoas { get; set; }
    }
}
