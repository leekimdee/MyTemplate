using Dapper;
using MyTemplate.Codes;
using MyTemplate.Models;
using Oracle.DataAccess.Client;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTemplate.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MyConnection cn = new MyConnection();
            List<NhanSu> nhansues = new List<NhanSu>();
            string strSql = "SELECT TOP 20 HoVaTenDem, Ten, GioiTinh, NgaySinh, PHONGBAN.TenPhongBan as PhongBan, DANTOC.TenDanToc as DanToc, TONGIAO.TenTonGiao as TonGiao FROM NHANSU inner join DANTOC on NHANSU.DanTocID=DANTOC.Id inner join TONGIAO on NHANSU.TonGiaoID=TONGIAO.Id inner join PHONGBAN on NHANSU.PhongBanId=PHONGBAN.Id";

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["QtDbConStr"].ConnectionString))
            {
                nhansues = db.Query<NhanSu>(strSql).ToList();
            }

            return View(nhansues);
        }

        public ActionResult About(int page = 1)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["QtDbConStr"].ConnectionString))
            {
                string strSql = "SELECT TOP 20 HoVaTenDem, Ten, GioiTinh, NgaySinh, PHONGBAN.TenPhongBan as PhongBan, DANTOC.TenDanToc as DanToc, TONGIAO.TenTonGiao as TonGiao FROM NHANSU inner join DANTOC on NHANSU.DanTocID=DANTOC.Id inner join TONGIAO on NHANSU.TonGiaoID=TONGIAO.Id inner join PHONGBAN on NHANSU.PhongBanId=PHONGBAN.Id";
                var nhansues = db.Query<NhanSu>(strSql);
                int itemsPerPage = 7;
                int start = (page - 1) * itemsPerPage;
                ViewBag.PageCount = Math.Ceiling(nhansues.Count() / (double)itemsPerPage);
                ViewBag.CurrentPage = page;
                var paginatedNhanSues = nhansues.OrderBy(p => p.Ten).Skip(start).Take(itemsPerPage);
                return View(paginatedNhanSues);
            }
        }

        public ActionResult Contact(int? page)
        {
            //ViewBag.Message = "Your contact page.";

            //List<Nguoi> nguoies = new List<Nguoi>();
            //string strSql = "select hoten, dm_chucvu.ten as chucvu, phongban.ten as phongban,ngaysinh, gioi_tinh as gioitinh from nguoi left join phongban on nguoi.id_phongban = phongban.id left join dm_chucvu on nguoi.id_chucvu = dm_chucvu.id where nguoi.is_deleted = 0";

            //using (IDbConnection db = new OracleConnection(ConfigurationManager.ConnectionStrings["QlmDbConStr"].ConnectionString))
            //{
            //    nguoies = db.Query<Nguoi>(strSql).ToList();
            //}

            //return View(nguoies);

            string strSql = "select hoten, dm_chucvu.ten as chucvu, phongban.ten as phongban,ngaysinh, gioi_tinh as gioitinh from nguoi left join phongban on nguoi.id_phongban = phongban.id left join dm_chucvu on nguoi.id_chucvu = dm_chucvu.id where nguoi.is_deleted = 0";
            using (IDbConnection db = new OracleConnection(ConfigurationManager.ConnectionStrings["QlmDbConStr"].ConnectionString))
            {
                var nguoies = db.Query<Nguoi>(strSql);
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(nguoies.ToPagedList(pageNumber, pageSize));
            }
        }
    }
}