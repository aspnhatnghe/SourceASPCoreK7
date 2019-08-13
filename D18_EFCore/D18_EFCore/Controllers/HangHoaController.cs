using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D18_EFCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace D18_EFCore.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly MyeStoreContext _context;
        public HangHoaController(MyeStoreContext db)
        {
            _context = db;
        }

        int SoPT1Trang = 6;
        public IActionResult Index(int page = 1)
        {
            ViewBag.TrangHienTai = page;
            ViewBag.TongSoTrang = Math.Ceiling(1.0 * _context.HangHoa.Count() / SoPT1Trang);

            var data = _context.HangHoa
                //Sắp giảm theo cột giảm giá
                .OrderByDescending(p => p.GiamGia)
                //nếu giảm giá thì sắp theo tên
                .ThenBy(p => p.TenHh)
                .Skip((page - 1) * SoPT1Trang)
                .Take(SoPT1Trang);

            return View(data);
        }


        #region Thống kê - Gom nhóm
        public IActionResult ThongKeTheoLoai()
        {
            var data = _context.HangHoa
                //gom nhóm theo loại
                .GroupBy(hh => hh.MaLoaiNavigation)
                //Lấy
                .Select(hh => new {
                    //ở đây .Key là đối tượng loại (MaLoaiNavigation)
                    hh.Key.TenLoai,
                    SoHangHoa = hh.Count(),
                    GiaLonNhat = hh.Max(p => p.DonGia)
                });
            return Json(data);
        }

        public IActionResult ThongKeBanHang()
        {
            var data = _context.ChiTietHd
                //gom nhóm theo hàng hóa và khách hàng
                .GroupBy(cthd => new {
                    cthd.MaHhNavigation,
                    cthd.MaHdNavigation.MaKhNavigation
                })
                //Lấy
                .Select(cthd => new {
                    cthd.Key.MaKhNavigation.HoTen,
                    cthd.Key.MaHhNavigation.TenHh,
                    TongTien = cthd.Sum(p => p.SoLuong * p.DonGia * (1 - p.GiamGia))
                });

            return Json(data);
        }

        public IActionResult ThongKeDoanhThu()
        {
            var data = _context.ChiTietHd                
                .GroupBy(hd => new {
                    hd.MaHdNavigation.MaKhNavigation,
                    hd.MaHdNavigation.NgayDat.Year,
                    hd.MaHdNavigation.NgayDat.Month
                })                
                .Select(hd => new {
                    hd.Key.MaKhNavigation.HoTen,
                    NamThang = $"{hd.Key.Year}/{hd.Key.Month}",
                    DoanhThu = hd.Sum(p => p.SoLuong * p.DonGia * (1 - p.GiamGia))
                });

            return Json(data);
        }
        #endregion
    }
}