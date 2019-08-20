using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFCore_DBFirst.Models;
using Microsoft.AspNetCore.Http;
using D21_Session.Helpers;

namespace EFCore_DBFirst.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult DemoSession()
        {
            //Tạo Session
            HttpContext.Session.SetString("Ten", "Nhất Nghệ");
            HttpContext.Session.SetInt32("Nam", 2003);

            Loai lo = new Loai
            {
                MaLoai = 1000,
                TenLoai = "Bia",
                MoTa = "Các loại bia"
            };
            //nhớ chèn namespace Project.Helpers
            HttpContext.Session.Set<Loai>("Loai", lo);

            return View();
        }
    }
}
