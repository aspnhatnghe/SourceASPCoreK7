using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D21_Session.Models;
using EFCore_DBFirst.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace D21_Session.Controllers
{
    public class DemoController : Controller
    {
        private readonly IHttpContextAccessor _httpContext;
        public DemoController(IHttpContextAccessor httpcontext)
        {
            _httpContext = httpcontext;
        }
        public IActionResult Index()
        {
            MySession session = new MySession(_httpContext);

            return Json(session.GetValue<Loai>("Loai"));
        }
    }
}