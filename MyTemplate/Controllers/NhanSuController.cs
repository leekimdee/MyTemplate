using Dapper;
using MyTemplate.Models;
using MyTemplate.Models.ViewModels;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace MyTemplate.Controllers
{
    public class NhanSuController : Controller
    {
        // GET: NhanSu
        public ActionResult Index(int page = 1)
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["QtDbConStr"].ConnectionString))
            {
                string strSql = "SELECT TOP 50 ROW_NUMBER() over(order by NHANSU.Id) as stt, HoVaTenDem, Ten, GioiTinh, NgaySinh, PHONGBAN.TenPhongBan as PhongBan, DANTOC.TenDanToc as DanToc, TONGIAO.TenTonGiao as TonGiao FROM NHANSU inner join DANTOC on NHANSU.DanTocID=DANTOC.Id inner join TONGIAO on NHANSU.TonGiaoID=TONGIAO.Id inner join PHONGBAN on NHANSU.PhongBanId=PHONGBAN.Id";
                var nhansues = db.Query<NhanSu>(strSql);

                NhanSuViewModel nhansuView = new NhanSuViewModel();
                nhansuView.ItemsPerPage = 10;
                nhansuView.NhanSues = nhansues;
                nhansuView.CurrentPage = page;
                return View(nhansuView);
            }
            return View();
        }

        public ActionResult ListAll()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            List<NhanSu> nhansues = new List<NhanSu>();
            string strSql = "SELECT TOP 20 ROW_NUMBER() over(order by NHANSU.Id) as stt, HoVaTenDem, Ten, GioiTinh, format(NgaySinh,'dd/MM/yyyy') as StrNgaySinh, PHONGBAN.TenPhongBan as PhongBan, DANTOC.TenDanToc as DanToc, TONGIAO.TenTonGiao as TonGiao FROM NHANSU inner join DANTOC on NHANSU.DanTocID=DANTOC.Id inner join TONGIAO on NHANSU.TonGiaoID=TONGIAO.Id inner join PHONGBAN on NHANSU.PhongBanId=PHONGBAN.Id";
            
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["QtDbConStr"].ConnectionString))
            {
                nhansues = db.Query<NhanSu>(strSql).ToList();
            }

            var nhansues_custom = nhansues.Select(s => new
            {
                STT = s.stt,
                HoVaTen = s.HoVaTenDem + " " + s.Ten,
                GioiTinh = s.GioiTinh == "True" ? "Nam" : "Nữ",
                NgaySinh = s.StrNgaySinh,
                PhongBan = s.PhongBan,
                DanToc = s.DanToc,
                TonGiao = s.TonGiao
            });
            return Json(nhansues_custom, JsonRequestBehavior.AllowGet);
        }
    }
}