using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyTemplate.Models;
using System.IO;

namespace MyTemplate.Controllers
{
    public class ImageController : Controller
    {
        private string _DbConStr;
        private readonly string _imageUploadFolder = "/UploadedFiles/images/";
        public ImageController()
        {
            _DbConStr = ConfigurationManager.ConnectionStrings["MyApplicationDbConStr"].ConnectionString;
        }
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            List<Image> images = new List<Image>();

            string strSql = "select * from Image order by Id desc";

            using (IDbConnection db = new SqlConnection(_DbConStr))
            {
                images = db.Query<Image>(strSql).ToList();
            }

            return Json(images, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetItemById(int ImageId)
        {
            Image image = new Image();
            string strSql = "select * from Image where Id = @Id";

            using (IDbConnection db = new SqlConnection(_DbConStr))
            {
                image = db.Query<Image>(strSql, new { Id = ImageId }).FirstOrDefault();
            }
            return Json(image);
        }

        public ActionResult Create()
        {
            GetImageAlbum();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Image image)
        {
            try
            {
                string strSql = "insert into Image (Title, ImageUrl, ImageAlbumId, CreatedDate, ModifiedDate, CreatedBy, ModifiedBy) VALUES (@Title, @ImageUrl, @ImageAlbumId, @CreatedDate, @ModifiedDate, @CreatedBy, @ModifiedBy)";
                int userId = 1;
                using (IDbConnection db = new SqlConnection(_DbConStr))
                {
                    db.Execute(strSql, new { Title = image.Title, ImageUrl = image.ImageUrl, ImageAlbumId = image.ImageAlbumId, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, CreatedBy = userId, ModifiedBy = userId });
                }

                TempData["SaveFlag"] = 1;
                return RedirectToAction("Index", "Image");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult CreateMulti()
        {
            GetImageAlbum();
            return View();
        }

        [HttpPost]
        public ActionResult CreateMulti(Image image, HttpPostedFileBase[] files)
        {
            try
            {
                string strSql = "insert into Image (Title, ImageUrl, ImageAlbumId, CreatedDate, ModifiedDate, CreatedBy, ModifiedBy) VALUES (@Title, @ImageUrl, @ImageAlbumId, @CreatedDate, @ModifiedDate, @CreatedBy, @ModifiedBy)";

                int userId = 1;
                using (IDbConnection db = new SqlConnection(_DbConStr))
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        image.ImageUrl = UploadFile(files[i]);
                        db.Execute(strSql, new { Title = image.Title, ImageUrl = image.ImageUrl, ImageAlbumId = image.ImageAlbumId, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, CreatedBy = userId, ModifiedBy = userId });
                    }
                }

                TempData["SaveFlag"] = 1;
                return RedirectToAction("Index", "Image");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult Update(int Id)
        {
            GetImageAlbum();

            string strSqlGetItemById = "select * from Image where Id=@Id";
            Image model;

            using (IDbConnection db = new SqlConnection(_DbConStr))
            {
                model = db.Query<Image>(strSqlGetItemById, new { Id = Id }).FirstOrDefault();
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Update(Image image)
        {
            try
            {
                string strSql = "update Image set Title = @Title, ImageUrl = @ImageUrl, ImageAlbumId = @ImageAlbumId, ModifiedDate = @ModifiedDate, ModifiedBy = @ModifiedBy where Id = @Id";

                using (IDbConnection db = new SqlConnection(_DbConStr))
                {
                    db.Execute(strSql, new { Id = image.Id, ImageUrl = image.ImageUrl,  Title = image.Title, ImageAlbumId = image.ImageAlbumId, ModifiedDate = DateTime.Now, ModifiedBy = image.ModifiedBy });
                }

                TempData["SaveFlag"] = 2;
                return RedirectToAction("Index", "Image");
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
                string strSql = "delete from image where Id = @Id";

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

        public ActionResult UploadImage()
        {
            return View();
        }

        public ActionResult ProcessUploadImage()
        {
            string imgUrl = "";
            try
            {
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    imgUrl = UploadFile(files[i]);
                }
            }
            catch(Exception ex)
            {
                return Content(ex.Message);
            }

            return Content(imgUrl);
        }

        private string UploadFile(HttpPostedFileBase file)
        {
            string fname = "";
            try
            {
                // Checking for Internet Explorer
                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                {
                    string[] testfiles = file.FileName.Split(new char[] { '\\' });
                    fname = testfiles[testfiles.Length - 1];
                }
                else
                {
                    fname = file.FileName;
                }

                // Get the complete folder path and store the file inside it.
                fname = Path.Combine(Server.MapPath(_imageUploadFolder), fname);
                file.SaveAs(fname);

            }
            catch (Exception ex)
            {

            }

            return _imageUploadFolder + file.FileName;
        }

        public ActionResult ShowGallery()
        {
            GetImageAlbum();
            return View();
        }

        public ActionResult GetImageByAlbumId(int albumId)
        {
            List<Image> images = new List<Image>();

            string strSql = "select * from Image where ImageAlbumId = @albumId";

            using (IDbConnection db = new SqlConnection(_DbConStr))
            {
                images = db.Query<Image>(strSql, new { albumId = albumId }).ToList();
            }

            return Json(images, JsonRequestBehavior.AllowGet);
        }

        void GetImageAlbum()
        {
            string strSql = "select * from ImageAlbum";

            using (IDbConnection db = new SqlConnection(_DbConStr))
            {
                ViewBag.ImageAlbums = db.Query<ImageAlbum>(strSql);
            }
        }
    }
}