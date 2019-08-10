using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EFCore_DBFirst.Models;
using EFCore_DBFirst.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EFCore_DBFirst.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly MyeStoreContext _context;
        private readonly IMapper _mapper;
        public HangHoaController(MyeStoreContext db, IMapper mapper)
        {
            _context = db; _mapper = mapper;
        }

        public IActionResult List(int loai, string nhacc)
        {
            var dsHangHoa = _context.HangHoa.AsQueryable();
            if(loai > 0)
            {
                dsHangHoa = dsHangHoa.Where(p => p.MaLoai == loai);
            }
            if(!string.IsNullOrEmpty(nhacc))
            {
                dsHangHoa = dsHangHoa.Where(p => p.MaNcc == nhacc);
            }

            var data = _mapper.Map<List<HangHoaViewModel>>(dsHangHoa.ToList());

            return View(data);
        }
    }
}