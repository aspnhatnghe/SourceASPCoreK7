using AutoMapper;
using EFCore_DBFirst.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore_DBFirst.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HangHoa, HangHoaViewModel>();
            //CreateMap<KhachHang, KhachHangRegiter>().ReverseMap();
        }
    }
}
