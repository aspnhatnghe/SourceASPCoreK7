using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using D14_ADONET.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace D14_ADONET.Controllers
{
    public class LoaiController : Controller
    {
        public IActionResult Index()
        {
            return View(LoaiDataAccessLayer.GetLoais());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Loai lo, IFormFile fHinh)
        {
            if (ModelState.IsValid)
            {
                if (fHinh != null)
                {
                    string fileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", "Loai", fHinh.FileName);
                    using (var file = new FileStream(fileName, FileMode.Create))
                    {
                        fHinh.CopyTo(file);

                        lo.Hinh = fHinh.FileName;
                    }
                }
                LoaiDataAccessLayer.AddLoai(lo);

                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int maloai)
        {
            Loai loai = LoaiDataAccessLayer.GetLoai(maloai);

            return View(loai);
        }

        [HttpPost]
        public IActionResult Edit(int maloai, Loai lo, IFormFile fHinh)
        {
            if (fHinh != null)
            {
                string fileName = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", "Loai", fHinh.FileName);
                using (var file = new FileStream(fileName, FileMode.Create))
                {
                    fHinh.CopyTo(file);

                    lo.Hinh = fHinh.FileName;
                }
            }
            LoaiDataAccessLayer.UpdateLoai(lo);

            return View(lo);
        }
    }
}