using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D22_Identity.ViewComponents
{
    public class CheckPermission : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContext;
        public CheckPermission(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public IViewComponentResult Invoke()
        {
            var controller = _httpContext.HttpContext.Request;

            //if()
            return View();

        }
    }
}
