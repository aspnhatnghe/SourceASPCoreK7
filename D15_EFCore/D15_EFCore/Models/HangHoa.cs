using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace D15_EFCore.Models
{
    [Table("HangHoa")]
    public class HangHoa
    {
        [Key]
        public int MaHh { get; set; }
        [MaxLength(50)]
        public string TenHh { get; set; }
        public int? SoLuong { get; set; }
        public double DonGia { get; set; }
        public double ThanhTien => SoLuong.Value * DonGia;

        public int MaLoai { get; set; }
        [ForeignKey("MaLoai")]
        public Loai Loai { get; set; }
        //public Loai MaLoaiNavigation { get; set; }
    }
}
