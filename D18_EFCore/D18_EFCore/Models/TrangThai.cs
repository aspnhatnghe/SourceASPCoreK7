using System;
using System.Collections.Generic;

namespace D18_EFCore.Models
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
