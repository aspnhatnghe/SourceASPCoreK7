using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D16_EFCore_DBFirst.Models
{
    public class HangHoaView
    {
        public int MaHh { get; set; }
        public string TenHh { get; set; }
        public double DonGia { get; set; }
        public double GiamGia { get; set; }
        public string Loai { get; set; }        
        public double GiaBan => DonGia * (1 - GiamGia);
        //public double GiaBan
        //{
        //    get
        //    {
        //        return DonGia * (1 - GiamGia);
        //    }
        //}
    }
}
