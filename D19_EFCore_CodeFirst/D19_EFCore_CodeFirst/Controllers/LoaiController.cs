using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D19_EFCore_CodeFirst.Models;
using Microsoft.AspNetCore.Mvc;

namespace D19_EFCore_CodeFirst.Controllers
{
    public class LoaiController : Controller
    {
        private readonly MyDbContext _context;
        public LoaiController(MyDbContext db)
        {
            _context = db;
        }
        public IActionResult Index()
        {
            return View(_context.Loais.ToList());
        }

        public IActionResult CreateOrEdit(int? id)
        {
            Loai lo = new Loai();
            if (id.HasValue)
            {
                lo = _context.Loais.SingleOrDefault(p => p.MaLoai == id.Value);
                if (lo == null)//tìm không thấy
                    lo = new Loai();
            }

            return View(lo);
        }

        public IActionResult Sua(int? id)
        {
            Loai lo = new Loai();
            if (id.HasValue)
            {
                lo = _context.Loais.SingleOrDefault(p => p.MaLoai == id.Value);
                if (lo == null)//tìm không thấy
                    lo = new Loai();
            }

            return PartialView("CreateOrEdit", lo);
        }

        [HttpPost]
        public IActionResult CreateOrEdit(int? id, Loai lo)
        {
            if (ModelState.IsValid)
            {
                if (id > 0 && id == lo.MaLoai)
                {
                    _context.Update(lo);
                }
                else
                {
                    _context.Add(lo);
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("CreateOrEdit", new { id = id });
        }

        public IActionResult Delete(int id)
        {
            Loai lo = _context.Loais.SingleOrDefault(p => p.MaLoai == id);
            if (lo != null)
            {
                _context.Remove(lo);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult DeleteAjax(int id)
        {
            Loai lo = _context.Loais.SingleOrDefault(p => p.MaLoai == id);
            if (lo == null)
            {
                return Json(new
                {
                    status = 1,
                    message = "Không có mã này"
                });
            }

            try
            {
                _context.Remove(lo); _context.SaveChanges();
                return Json(new { status = 2, message = "Xóa thành công" });
            }
            catch(Exception ex)
            {
                return Json(new { status = 1, message = "Xóa thất bại - " + ex.Message });
            }

        }
    }
}