using BanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        CECMSDbContext db;
        // GET: Admin/Account
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(Account model, FormCollection collection)
        {
            CECMSDbContext db = new CECMSDbContext();

            var NameUser = db.Accounts.Where(x => x.AccountName == model.AccountName && x.Status == 1).FirstOrDefault();
            Session["NameUser"] = NameUser;
            if (Session["NameUser"] == null)
            {
                ViewBag.Error = "Tên đăng nhập đã tồn tại";
                return View();  
            }
            else
            {
                var item = db.Accounts.Where(x => x.AccountName == model.AccountName && x.Password == model.Password && x.Status == 1).FirstOrDefault();

                if (item != null)
                {
                    if (item.DateIssued < DateTime.Now)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", " Tài khoản hết hiệu lực");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Thông tin tài khoản hoặc mật khẩu không chính xác");
                    return View();
                }
            }
            

        }
        public ActionResult Logout()
        {
            CECMSDbContext db = new CECMSDbContext();
            var NameUser = db.Accounts.FirstOrDefault();
            Session["NameUser"] = NameUser;
            Session.Remove("NameUser");
            //Session["UserName"] = null;
            return RedirectToAction("Login", "Account");
        }
    }
}