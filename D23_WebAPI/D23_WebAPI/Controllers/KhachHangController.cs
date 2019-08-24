using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D21_Session.ViewModels;
using D21_Session.Helpers;
using EFCore_DBFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace D21_Session.Controllers
{
    [Authorize]
    public class KhachHangController : Controller
    {
        private readonly MyeStoreContext _context;
        public KhachHangController(MyeStoreContext db)
        {
            _context = db;
        }

        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl = null)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl = null)
        {
            if (ModelState.IsValid)
            {
                KhachHang kh = _context.KhachHang.SingleOrDefault(p => p.MaKh == model.MaKH && p.MatKhau == model.MatKhau);
                if (kh != null)
                {
                    //Ghi nhận Session
                    HttpContext.Session.Set<KhachHang>("KhachHang", kh);

                    //khai báo thông tin Identity
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name,
kh.HoTen),
                        new Claim(ClaimTypes.Email, kh.Email),
                        new Claim(ClaimTypes.Role, "KhachHang")
                    };
                    // create identity
                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);


                    //nếu có trang yêu cầu trước đó
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }

                    return RedirectToAction("Profile", "KhachHang");
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            //Xóa Session
            HttpContext.Session.Remove("KhachHang");
            //HttpContext.Session.Clear();//xóa tất cả

            return RedirectToAction("Login");
        }

        public IActionResult Index()
        {
            //hiển thị danh sách khách hàng
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        [Authorize(Roles ="KhachHang")]
        public IActionResult Purchased()
        {
            //hàng đã mua
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Promotion()
        {
            //khuyến mãi
            return View();
        }
    }
}