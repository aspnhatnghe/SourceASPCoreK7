using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore_DBFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace D20_Ajax.Controllers
{
    public class AjaxController : Controller
    {
        private readonly MyeStoreContext _context;
        public AjaxController(MyeStoreContext db)
        {
            _context = db;
        }

        public IActionResult Search()
        {
            return View();
        }

        public IActionResult ProcessSearch(string keyword)
        {
            var data = _context.HangHoa
                .Include(hh => hh.MaLoaiNavigation)
                .Include(hh => hh.MaNccNavigation)
                .Where(hh => hh.TenHh.ToLower().Contains(keyword));
            return PartialView(data);
        }

        public IActionResult TimKiem()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchJson(string TuKhoa, double GiaTu, double GiaDen)
        {
            var data = _context.HangHoa
                .Include(hh => hh.MaLoaiNavigation)
                .AsQueryable();
            if(!string.IsNullOrEmpty(TuKhoa))
            {
                data = data.Where(hh => hh.TenHh.Contains(TuKhoa));
            }
            if(GiaTu > 0)
            {
                data = data.Where(hh => hh.DonGia >= GiaTu);
            }
            if (GiaDen > 0)
            {
                data = data.Where(hh => hh.DonGia <= GiaDen);
            }

            var result = data.Select(hh => new {
                TenHh = hh.TenHh,
                DonGia = hh.DonGia,
                Loai = hh.MaLoaiNavigation.TenLoai
            });
            return Json(result);
        }
    }
}