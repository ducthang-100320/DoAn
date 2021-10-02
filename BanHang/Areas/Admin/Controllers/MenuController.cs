using BanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        CECMSDbContext db;
        // GET: Admin/Menu
        public ActionResult Index()
        {
            ViewBag.Breadcrumb = "Danh mục sản phẩm";
            db = new CECMSDbContext();
            var model = db.Menus.ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Menu model, FormCollection collection)
        {
            CECMSDbContext db = new CECMSDbContext();
            
            model.Status = Convert.ToInt32(collection["cboStatus"].ToString());
            db.Menus.Add(model);
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
            var model = db.Banks.First(m => m.ID == id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Bank model, FormCollection collection)
        {
            db = new CECMSDbContext();
            var item = db.Banks.Find(model.ID);
            item.BankName = model.BankName;
            item.Status = model.Status = Convert.ToInt32(collection["cboStatus"].ToString());
            item.AccountNumber = model.AccountNumber;
            item.AccountName = model.AccountName;
            item.Branch = model.Branch;
            var Result = db.SaveChanges();

            if (Result >= 0)
            {
                ViewBag.SuccessEdit = "Cập nhật dữ liệu thành công";
                return View();

            }
            else
            {
                ModelState.AddModelError("error", "Lỗi trong quá trình ghi dữ liệu");
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}