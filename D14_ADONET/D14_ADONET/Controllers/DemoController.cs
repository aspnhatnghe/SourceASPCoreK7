using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace D14_ADONET.Controllers
{
    public class DemoController : Controller
    {
        string connectionString = @"Data Source=.;Initial Catalog=MyeStore;Integrated Security=True";
        public IActionResult GetLoai()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string sql = "SELECT * FROM Loai ORDER BY TenLoai DESC";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, connection);

            DataTable dtLoai = new DataTable();
            dataAdapter.Fill(dtLoai);

            //-----Xử lý kết quả
            StringBuilder sb = new StringBuilder();
            foreach(DataRow row in dtLoai.Rows)
            {
                sb.Append($"{row["MaLoai"]} - {row["TenLoai"]}<br>");
            }

            return View("GetLoai", sb.ToString());
        }

        public IActionResult ReadSetting()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("myappsettings.json")
                .Build();

            ViewBag.Ten = builder["Ten"];
            ViewBag.Nam = builder["Nam"];
            ViewBag.Config1 = builder["MyConfig:Config1"];
            ViewBag.Config2 = builder["MyConfig:Config2"];
            ViewBag.DB1 = builder.GetConnectionString("DB1");
            return View();
        }
    }
}