using AutoMapper;
using EFCore_DBFirst.Models;
using EFCore_DBFirst.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore_DBFirst.ViewComponents
{
    public class HangDacBiet : ViewComponent
    {
        private readonly MyeStoreContext _context;
        private readonly IMapper _mapper;
        public HangDacBiet(MyeStoreContext db, IMapper mapper)
        {
            _context = db; _mapper = mapper;
        }

        public IViewComponentResult Invoke(int SoLuong = 5)
        {
            var dsHangHoa = _context.HangHoa
                .OrderByDescending(p => p.DonGia)
                //Lấy n sản phẩm từ danh sách
                .Take(SoLuong);

            var data = _mapper.Map<List<HangHoaViewModel>>(dsHangHoa.ToList());

            return View(data);
        }
    }
}
