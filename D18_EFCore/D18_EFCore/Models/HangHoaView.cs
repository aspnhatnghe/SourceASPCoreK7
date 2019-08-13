using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace D18_EFCore.Models
{
    public class HangHoaView
    {
        [Key]
        public int MaHh { get; set; }
        public string TenHh { get; set; }
        public double DonGia { get; set; }
    }
}
