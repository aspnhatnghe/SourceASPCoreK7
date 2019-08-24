using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D20_Ajax.Models;
using EFCore_DBFirst.Models;
using Microsoft.AspNetCore.Mvc;

namespace D20_Ajax.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly MyeStoreContext _context;
        public HangHoaController(MyeStoreContext db)
        {
            _context = db;
        }

        const int SoSP1Trang = 9;

        public IActionResult Index()
        {
            ViewBag.TongSoTrang = (int)Math.Ceiling(1.0 * _context.HangHoa.Count() / SoSP1Trang);

            return View();
        }

        public IActionResult LoadPage(int page  = 0)
        {
            var data = _context.HangHoa
                .Skip(page * SoSP1Trang)
                .Take(SoSP1Trang)
                .Select(hh =>  new HangHoaViewModel {
                    MaHh = hh.MaHh,
                    TenHh = hh.TenHh,
                    Hinh = hh.Hinh,
                    DonGia = hh.DonGia.Value
                });

            return PartialView(data);
        }
    }
}