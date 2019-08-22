using EFCore_DBFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D21_Session.Models
{
    public class CartItem
    {
        public HangHoa HangMua { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien => SoLuong * HangMua.DonGia.Value;
    }
}
