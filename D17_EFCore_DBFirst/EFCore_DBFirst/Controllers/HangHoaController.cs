using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore_DBFirst.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCore_DBFirst.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly MyeStoreContext _context;
        public HangHoaController(MyeStoreContext db)
        {
            _context = db;
        }

        public IActionResult List(int loai, string nhacc)
        {
            var dsHangHoa = _context.HangHoa.AsQueryable();
            if(loai > 0)
            {
                dsHangHoa = dsHangHoa.Where(p => p.MaLoai == loai);
            }
            if(!string.IsNullOrEmpty(nhacc))
            {
                dsHangHoa = dsHangHoa.Where(p => p.MaNcc == nhacc);
            }

            return View(dsHangHoa);
        }
    }
}