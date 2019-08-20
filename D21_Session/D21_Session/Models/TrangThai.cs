using System;
using System.Collections.Generic;

namespace EFCore_DBFirst.Models
{
    public partial class TrangThai
    {
        public TrangThai()
        {
            HoaDon = new HashSet<HoaDon>();
        }

        public int MaTrangThai { get; set; }
        public string TenTrangThai { get; set; }
        public string MoTa { get; set; }

        public virtual ICollection<HoaDon> HoaDon { get; set; }
    }
}
