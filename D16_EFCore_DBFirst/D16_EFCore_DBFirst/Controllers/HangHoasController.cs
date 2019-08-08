using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using D16_EFCore_DBFirst.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace D16_EFCore_DBFirst.Controllers
{
    public class HangHoasController : Controller
    {
        private readonly MyeStoreContext _context;

        public HangHoasController(MyeStoreContext context)
        {
            _context = context;
        }

        public IActionResult TimKiem()
        {
            return View();
        }

        public IActionResult Search(string TuKhoa, double GiaTu, double GiaDen)
        {
            /*
            var dsHangHoa = _context.HangHoa
                //.Where(): lọc theo điều kiện, trả về rỗng, hoặc 1, hoặc nhiều phần tử
                .Where(hh => hh.TenHh.Contains(TuKhoa) && hh.DonGia >= GiaTu && hh.DonGia <= GiaDen);
                */

            var dsHangHoa = _context.HangHoa.AsQueryable();
            if(!string.IsNullOrEmpty(TuKhoa))
            {
                dsHangHoa = dsHangHoa.Where(hh => hh.TenHh.Contains(TuKhoa));
            }
            if(GiaTu > 0)
            {
                dsHangHoa = dsHangHoa.Where(p => p.DonGia >= GiaTu);
            }
            if (GiaDen > 0)
            {
                dsHangHoa = dsHangHoa.Where(p => p.DonGia <= GiaDen);
            }

            //.Select() : định nghĩa dữ liệu lấy ra
            var result = dsHangHoa.Select(p=> new HangHoaView
            {
                MaHh = p.MaHh,
                TenHh = p.TenHh,
                DonGia = p.DonGia.Value,
                GiamGia = p.GiamGia,
                Loai = p.MaLoaiNavigation.TenLoai
            }).ToList();

            return View("TimKiem", result);
        }

        // GET: HangHoas
        public async Task<IActionResult> Index()
        {
            var myeStoreContext = _context.HangHoa
                //lấy thông tin loại
                .Include(h => h.MaLoaiNavigation)
                .Include(h => h.MaNccNavigation)
                //sắp xếp giảm dần theo mã
                .OrderByDescending(p => p.MaHh);
            return View(await myeStoreContext.ToListAsync());
        }

        // GET: HangHoas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoa
                .Include(h => h.MaLoaiNavigation)
                .Include(h => h.MaNccNavigation)
                .FirstOrDefaultAsync(m => m.MaHh == id);
            if (hangHoa == null)
            {
                return NotFound();
            }

            return View(hangHoa);
        }

        // GET: HangHoas/Create
        public IActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(_context.Loai, "MaLoai", "TenLoai");
            ViewBag.MaNcc = new SelectList(_context.NhaCungCap, "MaNcc", "TenCongTy");
            return View();
        }

        // POST: HangHoas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HangHoa hangHoa, IFormFile fHinh)
        {
            if (ModelState.IsValid)
            {
                hangHoa.Hinh = MyTool.UploadImage(fHinh);

                _context.Add(hangHoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoai"] = new SelectList(_context.Loai, "MaLoai", "TenLoai", hangHoa.MaLoai);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCap, "MaNcc", "TenCongTy", hangHoa.MaNcc);
            return View(hangHoa);
        }

        // GET: HangHoas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoa.FindAsync(id);
            if (hangHoa == null)
            {
                return NotFound();
            }
            ViewData["MaLoai"] = new SelectList(_context.Loai, "MaLoai", "TenLoai", hangHoa.MaLoai);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCap, "MaNcc", "TenCongTy", hangHoa.MaNcc);
            return View(hangHoa);
        }

        // POST: HangHoas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaHh,TenHh,MaLoai,MoTaDonVi,DonGia,Hinh,NgaySx,GiamGia,SoLanXem,MoTa,MaNcc")] HangHoa hangHoa, IFormFile fHinh)
        {
            if (id != hangHoa.MaHh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (fHinh != null)
                {
                    hangHoa.Hinh = MyTool.UploadImage(fHinh);
                }

                try
                {
                    _context.Update(hangHoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HangHoaExists(hangHoa.MaHh))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoai"] = new SelectList(_context.Loai, "MaLoai", "TenLoai", hangHoa.MaLoai);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCap, "MaNcc", "MaNcc", hangHoa.MaNcc);
            return View(hangHoa);
        }

        // GET: HangHoas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.HangHoa
                .Include(h => h.MaLoaiNavigation)
                .Include(h => h.MaNccNavigation)
                .FirstOrDefaultAsync(m => m.MaHh == id);
            if (hangHoa == null)
            {
                return NotFound();
            }

            return View(hangHoa);
        }

        // POST: HangHoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hangHoa = await _context.HangHoa.FindAsync(id);
            _context.HangHoa.Remove(hangHoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HangHoaExists(int id)
        {
            return _context.HangHoa.Any(e => e.MaHh == id);
        }
    }
}
