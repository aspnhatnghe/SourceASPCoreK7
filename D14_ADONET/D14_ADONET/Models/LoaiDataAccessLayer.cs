using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace D14_ADONET.Models
{
    public class LoaiDataAccessLayer
    {
        public static List<Loai> GetLoais()
        {
            List<Loai> dsLoai = new List<Loai>();

            DataTable dtLoai = DataProvider.SelectData("spLayTatCaLoai", CommandType.StoredProcedure, null);

            foreach (DataRow row in dtLoai.Rows)
            {
                dsLoai.Add(new Loai
                {
                    MaLoai = Convert.ToInt32(row["MaLoai"]),
                    TenLoai = row["TenLoai"].ToString(),
                    MoTa = row["MoTa"].ToString(),
                    Hinh = row["Hinh"].ToString()
                });
            }

            return dsLoai;
        }

        public static Loai GetLoai(int? id)
        {
            if (!id.HasValue) return null;

            SqlParameter[] pa = new SqlParameter[1];
            pa[0] = new SqlParameter("MaLoai", id.Value);

            DataTable loai = DataProvider.SelectData("spLayLoai", CommandType.StoredProcedure, pa);

            if (loai.Rows.Count == 0) return null;

            DataRow row = loai.Rows[0];
            return new Loai
            {
                MaLoai = Convert.ToInt32(row["MaLoai"]),
                TenLoai = row["TenLoai"].ToString(),
                MoTa = row["MoTa"].ToString(),
                Hinh = row["Hinh"].ToString()
            };
        }

        public static void AddLoai(Loai lo)
        {
            SqlParameter[] pa = new SqlParameter[4];
            pa[0] = new SqlParameter("MaLoai", SqlDbType.Int);
            pa[0].Direction = ParameterDirection.Output;
            pa[1] = new SqlParameter("TenLoai", lo.TenLoai);
            pa[2] = new SqlParameter("MoTa", lo.MoTa);
            pa[3] = new SqlParameter("Hinh", lo.Hinh);
            DataProvider.ExcuteNonQuery("spThemLoai", CommandType.StoredProcedure, pa);
        }

        public static void UpdateLoai(Loai lo)
        {
            SqlParameter[] pa = new SqlParameter[4];
            pa[0] = new SqlParameter("MaLoai", lo.MaLoai);
            pa[1] = new SqlParameter("TenLoai", lo.TenLoai);
            pa[2] = new SqlParameter("MoTa", lo.MoTa);
            pa[3] = new SqlParameter("Hinh", lo.Hinh);
            DataProvider.ExcuteNonQuery("spSuaLoai", CommandType.StoredProcedure, pa);
        }

        public static void DeleteLoai(int? id)
        {
            if (!id.HasValue) return;
            SqlParameter[] pa = new SqlParameter[1];
            pa[0] = new SqlParameter("MaLoai", id);
            DataTable dtLoai = DataProvider.SelectData("spXoaLoai",
           CommandType.StoredProcedure, pa);
        }
    }
}
