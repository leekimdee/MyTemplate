using Dapper;
using MyTemplate.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace MyTemplate.Controllers
{
    public class PhongBanController : Controller
    {
        // GET: PhongBan
        public ActionResult Index()
        {
            List<PhongBan> phongbans = new List<PhongBan>();
            string strSql = "SELECT ROW_NUMBER() over(order by Id) as stt, Id, TenPhongBan, KiHieu, SapXepPhong FROM PHONGBAN";

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["QtDbConStr"].ConnectionString))
            {
                phongbans = db.Query<PhongBan>(strSql).ToList();
            }

            return View("~/Views/PhongBan/PhongBan.cshtml", phongbans);
        }

        public ActionResult GetItemById(int PhongBanId)
        {
            PhongBan phongban = new PhongBan();
            string strSql = "SELECT * FROM PHONGBAN WHERE Id = @Id";

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["QtDbConStr"].ConnectionString))
            {
                phongban = db.Query<PhongBan>(strSql, new { Id = PhongBanId}).FirstOrDefault();
            }
            return Json(phongban);
        }

        public ActionResult Create(PhongBan phongban)
        {
            try
            {
                string strSql = "INSERT INTO PHONGBAN (TenPhongBan, KiHieu, SapXepPhong) VALUES (@TenPhongBan, @KiHieu, @SapXepPhong)";

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["QtDbConStr"].ConnectionString))
                {
                    db.Execute(strSql, new { TenPhongBan = phongban.TenPhongBan, KiHieu = phongban.KiHieu, SapXepPhong = phongban.SapXepPhong });
                }

                return Content("0");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult Update(PhongBan phongban)
        {
            try
            {
                string strSql = "UPDATE PHONGBAN SET TenPhongBan = @TenPhongBan, KiHieu = @KiHieu, SapXepPhong = @SapXepPhong WHERE Id = @Id";

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["QtDbConStr"].ConnectionString))
                {
                    db.Execute(strSql, new { Id = phongban.Id, TenPhongBan = phongban.TenPhongBan, KiHieu=phongban.KiHieu, SapXepPhong=phongban.SapXepPhong });
                }

                return Content("0");
            }
            catch(Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult Delete(int Id)
        {
            try
            {
                string strSql = "DELETE FROM PHONGBAN WHERE Id = @Id";

                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["QtDbConStr"].ConnectionString))
                {
                    db.Execute(strSql, new { Id = Id });
                }

                return Content("0");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}