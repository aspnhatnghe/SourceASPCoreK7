using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D21_Session.ViewModels;
using D21_Session.Helpers;
using EFCore_DBFirst.Models;
using Microsoft.AspNetCore.Mvc;

namespace D21_Session.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly MyeStoreContext _context;
        public KhachHangController(MyeStoreContext db)
        {
            _context = db;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                KhachHang kh = _context.KhachHang.SingleOrDefault(p => p.MaKh == model.MaKH && p.MatKhau == model.MatKhau);
                if(kh != null)
                {
                    //Ghi nhận Session
                    HttpContext.Session.Set<KhachHang>("KhachHang", kh);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            //Xóa Session
            HttpContext.Session.Remove("KhachHang");
            //HttpContext.Session.Clear();//xóa tất cả

            return RedirectToAction("Login");
        }
    }
}