using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D21_Session.Helpers;

namespace D21_Session.Models
{
    public class MySession
    {
        private readonly IHttpContextAccessor _httpContext;
        public MySession(IHttpContextAccessor httpcontext)
        {
            _httpContext = httpcontext;
        }
        public void SetValue<T>(string key, T value)
        {
            _httpContext.HttpContext.Session.Set<T>(key, value);
        }

        public T GetValue<T>(string key)
        {
            return _httpContext.HttpContext.Session.Get<T>(key);
        }
    }
}
