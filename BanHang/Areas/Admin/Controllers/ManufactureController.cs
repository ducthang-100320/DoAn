using BanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Areas.Admin.Controllers
{
    public class ManufactureController : Controller
    {
        CECMSDbContext db;
        // GET: Admin/Manufacture
        public ActionResult Index()
        {
            ViewBag.Breadcrumb = "Hãng Sản Phẩm";
            db = new CECMSDbContext();
            var LoaiSanPham = db.ProductCategories.ToList();
            Session["LoaiSanPham"] = LoaiSanPham;
            var model = db.Manufacturers.ToList();
            return View(model);

        }
        [HttpGet]
        public ActionResult Create()
        {
            db = new CECMSDbContext();
            var LoaiSanPham = db.ProductCategories.ToList();
            Session["LoaiSanPham"] = LoaiSanPham;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Manufacturer model, HttpPostedFile filePost, FormCollection collection)
        {
            CECMSDbContext db = new CECMSDbContext();
            string fileLocation = "";
            if (Request.Files["filePost"].ContentLength <= 0) { model.Image = ""; }
            //model.Image = "../assets/images/user_pic.jfif";
            ModelState["filePost"].Errors.Clear();
            if (Request.Files["filePost"].ContentLength > 0)
            {
                string fileExtension = System.IO.Path.GetExtension(Request.Files["filePost"].FileName);
                fileLocation = Server.MapPath("~/Content/") + Request.Files["filePost"].FileName;
                if (System.IO.File.Exists(fileLocation))
                {
                    System.IO.File.Delete(fileLocation);
                }
                Request.Files["filePost"].SaveAs(fileLocation);

            }
            int iContent = fileLocation.IndexOf("Content");
            if (iContent > 0)
            {
                model.Image = @"\" + fileLocation.Substring(iContent, fileLocation.Length - iContent);
            }
            model.CategoryID = Convert.ToInt32(collection["cboLoaiSanPham"].ToString());
            model.Status = Convert.ToInt32(collection["cboStatus"].ToString());
            var manufacture = db.Manufacturers.Where(x => x.Name.Equals(model.Name) && x.CategoryID == model.CategoryID).ToList();
                foreach (var itemmanu in manufacture)
                {
                    if (model.Name.Equals(itemmanu.Name) && model.CategoryID == itemmanu.CategoryID)
                    {
                    ViewBag.Error = "Hãng sản phẩm này đã tồn tại trong hệ thống";
                    return View();
                    }


                } 
                
                db.Manufacturers.Add(model);
                var Result = db.SaveChanges();
                if (Result >= 0)
                {
                    ViewBag.SuccessCreate = "Thêm thông tin thành công";
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Chưa nhập đầy đủ thông tin hãng sản phẩm");
                    return View();
                }
            
        }
        public ActionResult Edit(int id)
        {
            db = new CECMSDbContext();
            var LoaiSanPham = db.ProductCategories.ToList();
            Session["LoaiSanPham"] = LoaiSanPham;
            var model = db.Manufacturers.First(m => m.ID == id);
            Manufacturer manufacturer = new Manufacturer();
            manufacturer.Image = model.Image;
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Manufacturer model, FormCollection collection, HttpPostedFile filePost)
        {
            db = new CECMSDbContext();


            var item = db.Manufacturers.Find(model.ID);
            item.Name = model.Name;

            if (collection["cboLoaiSanPham"].ToString().Trim() != "")
                item.CategoryID = Convert.ToInt32(collection["cboLoaiSanPham"].ToString());
            else
                item.CategoryID = -1;
            item.Status = model.Status = Convert.ToInt32(collection["cboStatus"].ToString());
            item.Description = model.Description;

            string fileLocation = "";
            if (Request.Files["filePost"].ContentLength > 0)
            {
                string fileExtension = System.IO.Path.GetExtension(Request.Files["filePost"].FileName);
                fileLocation = Server.MapPath("~/Content/") + Request.Files["filePost"].FileName;
                //fileLocation = fileLocation.Replace(".xlsx", DateTime.Now + ".xlsx");

                if (System.IO.File.Exists(fileLocation))
                {
                    System.IO.File.Delete(fileLocation);
                }
                Request.Files["filePost"].SaveAs(fileLocation);
                int iContent = fileLocation.IndexOf("Content");
                if (iContent > 0)
                {
                    item.Image = @"\" + fileLocation.Substring(iContent, fileLocation.Length - iContent);
                }

                else
                {
                    item.Image = model.Image;
                }  
            }
            var Result = db.SaveChanges();

            if (Result >= 0)
            {
                ViewBag.SuccessEdit = "Cập nhật dữ liệu thành công";
                //return RedirectToAction("Edit", "Manufacture");
                //return RedirectToAction("Index", "Manufacture");
                return View(model);

            }
            else
            {
                ModelState.AddModelError("error", "Lỗi trong quá trình ghi dữ liệu");
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                db = new CECMSDbContext();
                //var ID = Convert.ToInt32(id);
                var item = db.Manufacturers.Find(id);
                if (item != null)
                {
                    db.Manufacturers.Remove(item);
                    db.SaveChanges();
                }

                return RedirectToAction("Index", "Manufacture");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
    }
}