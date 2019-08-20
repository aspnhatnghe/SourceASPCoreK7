using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore_DBFirst.Models;
using Microsoft.AspNetCore.Mvc;
using D21_Session.Helpers;
using D21_Session.Models;

namespace D21_Session.Controllers
{
    public class CartController : Controller
    {
        private readonly MyeStoreContext _context;
        public CartController(MyeStoreContext db)
        {
            _context = db;
        }

        public List<CartItem> Cart
        {
            get
            {
                var gh = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (gh == default(List<CartItem>))
                    gh = new List<CartItem>();
                return gh;
            }
        }
        public IActionResult Index()
        {
            return View(Cart);
        }

        public IActionResult AddToCart(int maHh, int soLuong = 1)
        {
            //kiểm tra đã có hàng hóa có maHh trong giỏ hàng chưa
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.HangMua.MaHh == maHh);

            if(item != null)//đã có
            {
                item.SoLuong += soLuong;
            }
            else//chưa có
            {
                //tạo mới
                HangHoa hh = _context.HangHoa.SingleOrDefault(p => p.MaHh == maHh);
                item = new CartItem
                {
                    HangMua = hh, SoLuong = soLuong
                };
                //thêm vào giỏ                
                gioHang.Add(item);
            }

            //lưu session
            HttpContext.Session.Set("GioHang", gioHang);

            return RedirectToAction("Index");
        }
    }
}