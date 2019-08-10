using EFCore_DBFirst.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore_DBFirst.ViewComponents
{
    public class NhaCungCap : ViewComponent
    {
        private readonly MyeStoreContext _context;
        public NhaCungCap(MyeStoreContext db)
        {
            _context = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var nhacc = _context.NhaCungCap
                //lọc lấy nhà cung cấp có hàng hóa
                .Where(p => p.HangHoa.Count > 0)
                .OrderBy(p => p.TenCongTy);

            return View(nhacc);
        }
    }
}
