
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace D16_EFCore_DBFirst.Models
{
    public class MyTool
    {
        public static string UploadImage(IFormFile fHinh, string folder = "HangHoa")
        {
            string fileName = string.Empty;
            if (fHinh != null)
            {
                fileName = DateTime.Now.Ticks.ToString() + fHinh.FileName;

                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", folder, fileName);

                using (var file = new FileStream(fullPath, FileMode.Create))
                {
                    fHinh.CopyTo(file);
                }
            }

            return fileName;
        }
    }
}
