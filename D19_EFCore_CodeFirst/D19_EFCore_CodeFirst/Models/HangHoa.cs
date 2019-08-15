using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace D19_EFCore_CodeFirst.Models
{
    [Table("HangHoa")]
    public class HangHoa
    {
        [Key]
        public int MaHH { get; set; }
        [Required]
        [MaxLength(50)]
        public string TenHH { get; set; }
        public string Hinh { get; set; }
        public string MoTa { get; set; }
        [Range(0, double.MaxValue)]
        public double DonGia { get; set; }
        [Range(0, int.MaxValue)]
        public int SoLuong { get; set; }

        public int MaLoai { get; set; }
        [ForeignKey("MaLoai")]
        public Loai Loai { get; set; }
    }
}