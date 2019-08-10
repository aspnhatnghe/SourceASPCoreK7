using EFCore_DBFirst.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore_DBFirst.ViewComponents
{
    public class Loai : ViewComponent
    {
        private readonly MyeStoreContext _context;
        public Loai(MyeStoreContext db)
        {
            _context = db;
        }

        public IViewComponentResult Invoke()
        {
            var loais = _context.Loai
                //lọc lấy loại có hàng hóa
                .Where(p => p.HangHoa.Count > 0)
                .OrderBy(p => p.TenLoai);

            return View("Default", loais);
        }
    }
}
