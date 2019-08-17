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
    }
}