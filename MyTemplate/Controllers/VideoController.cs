using Dapper;
using MyTemplate.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTemplate.Controllers
{
    public class VideoController : Controller
    {
        private string _DbConStr;

        public VideoController()
        {
            _DbConStr = ConfigurationManager.ConnectionStrings["MyApplicationDbConStr"].ConnectionString;
        }

        // GET: Video
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            List<Video> videos = new List<Video>();

            string strSql = "select * from Video order by Id desc";

            using (IDbConnection db = new SqlConnection(_DbConStr))
            {
                videos = db.Query<Video>(strSql).ToList();
            }

            return Json(videos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetItemById(int VideoId)
        {
            Video video = new Video();
            string strSql = "select * from Video where Id = @Id";

            using (IDbConnection db = new SqlConnection(_DbConStr))
            {
                video = db.Query<Video>(strSql, new { Id = VideoId }).FirstOrDefault();
            }
            return Json(video);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Video video)
        {
            try
            {
                string strSql = "insert into Video (Title, VideoUrl, CreatedDate, ModifiedDate, CreatedBy, ModifiedBy) VALUES (@Title, @VideoUrl, @CreatedDate, @ModifiedDate, @CreatedBy, @ModifiedBy)";
                int userId = 1;
                using (IDbConnection db = new SqlConnection(_DbConStr))
                {
                    db.Execute(strSql, new { Title = video.Title, VideoUrl = video.VideoUrl, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, CreatedBy = userId, ModifiedBy = userId });
                }

                TempData["SaveFlag"] = 1;
                return RedirectToAction("Index", "Video");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult Update(int Id)
        {
            string strSqlGetItemById = "select * from Video where Id=@Id";
            Video model;

            using (IDbConnection db = new SqlConnection(_DbConStr))
            {
                model = db.Query<Video>(strSqlGetItemById, new { Id = Id }).FirstOrDefault();
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Update(Video video)
        {
            try
            {
                string strSql = "update Video set Title = @Title, VideoUrl = @VideoUrl, ModifiedDate = @ModifiedDate, ModifiedBy = @ModifiedBy where Id = @Id";

                using (IDbConnection db = new SqlConnection(_DbConStr))
                {
                    db.Execute(strSql, new { Id = video.Id, VideoUrl = video.VideoUrl, Title = video.Title, ModifiedDate = DateTime.Now, ModifiedBy = video.ModifiedBy });
                }

                TempData["SaveFlag"] = 2;
                return RedirectToAction("Index", "Video");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult Delete(int Id)
        {
            try
            {
                string strSql = "delete from Video where Id = @Id";

                using (IDbConnection db = new SqlConnection(_DbConStr))
                {
                    db.Execute(strSql, new { Id = Id });
                }

                TempData["SaveFlag"] = 3;
                return Content("0");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult ShowVideo()
        {
            return View();
        }
    }
}