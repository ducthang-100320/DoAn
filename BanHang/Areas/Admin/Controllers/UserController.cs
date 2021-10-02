using BanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        CECMSDbContext db;
        // GET: Admin/User
        public ActionResult Index()
        {
            ViewBag.Breadcrumb = "Người Dùng";
            CECMSDbContext db = new CECMSDbContext();
            var model = (from a in db.Accounts
                         join b in db.Users

                         on a.ID equals b.Account_ID

                         select new
                         {
                             ID = a.ID,
                             Image = b.Image,
                             AccountName = a.AccountName,
                             UserName = b.UserName,
                             Sex = b.Sex,
                             Birthday = b.Birthday,
                             Address = b.Address,
                             Phone = b.Phone,
                             Email = b.Email
                             
                         }
                        ).ToList();
            List<AccountUser> lstAU = new List<AccountUser>();
            foreach (var item in model)
            {
                AccountUser _AccountUser = new AccountUser();
                _AccountUser._Account = new Account();
                _AccountUser._User = new User();

                _AccountUser._Account.ID = item.ID;
                _AccountUser._Account.AccountName = item.AccountName;
                _AccountUser._User.Image = item.Image;
                _AccountUser._User.UserName = item.UserName;
                _AccountUser._User.Sex = item.Sex;
                _AccountUser._User.Birthday = item.Birthday;
                _AccountUser._User.Address = item.Address;
                _AccountUser._User.Phone = item.Phone;
                _AccountUser._User.Email = item.Email;
            
                lstAU.Add(_AccountUser);

            }
            return View(lstAU);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            db = new CECMSDbContext();
            var NameUser = Session["NameUser"] as BanHang.Models.Account;
            var item1 = db.Accounts.Find(id);
            var item2 = db.Users.Where(x =>x.Account_ID == id).FirstOrDefault();
            AccountUser model = new AccountUser();
            model._Account.AccountName = item1.AccountName;
            model._User.Account_ID = item2.ID;
            model._User.Address = item2.Address;
            model._User.Email = item2.Email;
            model._User.Image = item2.Image;
            model._User.Phone = item2.Phone;
            model._User.Birthday = item2.Birthday;
            model._Account.Status = model._User.Status = item2.Status;
            model._Account.CreatedDate = model._User.CreatedDate= DateTime.Now;
            model._Account.CreatedUser = model._User.CreatedUser = NameUser.AccountName;
            model._Account.DateIssued  = item1.DateIssued;
            model._User.Description = item2.Description;
            model._Account.ModifiedDate = model._User.ModifiedDate = DateTime.Now;
            model._Account.ModifiedUser = model._User.ModifiedUser = NameUser.AccountName;
            model._User.Sex = item2.Sex;
            model._User.UserName = item2.UserName;

            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(AccountUser item, HttpPostedFile filePost, FormCollection collection)
        {


            CECMSDbContext db = new CECMSDbContext();
            var acc = db.Accounts.Find(item._Account.ID);

            acc.AccountName = item._Account.AccountName;
            //acc.CreatedDate = item.CreatedDate;
            //acc.CreatedUser = item.CreatedUser;
            acc.DateIssued = item._Account.DateIssued;
            acc.ModifiedDate = DateTime.Now;
            acc.ModifiedUser = "ducthang";
            acc.CreatedDate = DateTime.Now;
            acc.CreatedUser = "ducthang";
            acc.Password = item._Account.Password;
            acc.Status = -1;
            //var uResult = db.SaveChanges();
            var user = db.Users.Where(x => x.Account_ID == item._User.Account_ID).SingleOrDefault();
            user.UserName = item._User.UserName;
            user.Birthday = item._User.Birthday;

            


            var GioiTinh = collection["customRadio"];
            if (collection["customRadio"].ToString().Trim() != "Nam")
                item._User.Sex = Convert.ToInt32(collection["customRadio"].ToString());
            else
                item._User.Sex = 0;
            //user.Sex = item.Sex;
            user.Status = -1;
            user.Address = item._User.Address;
            user.Email = item._User.Email;
            user.Phone = item._User.Phone;
            string filelocation = "";
            if (Request.Files["filePost"].ContentLength > 0)
            {
                string fileExtenSion = System.IO.Path.GetExtension(Request.Files["filePost"].FileName);
                filelocation = Server.MapPath("~/Content/") + Request.Files["filePost"].FileName;

                if (System.IO.File.Exists(filelocation))
                {
                    System.IO.File.Exists(filelocation);
                }
                Request.Files["filePost"].SaveAs(filelocation);
                int icontent = filelocation.IndexOf("Content");
                if (icontent > 0)
                {
                    user.Image = @"\" + filelocation.Substring(icontent, filelocation.Length - icontent);
                }
                else
                {
                    user.Image = item._User.Image;
                }


                var uResult = db.SaveChanges();
                if (uResult >= 0)
                {
                    //SetAlert("success", "Cập nhật dữ liệu thành công");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    //SetAlert()
                    return View();
                }



            }
            return View();
        }
        public ActionResult Delete(Account model, int id)
        {
            try
            {
                db = new CECMSDbContext();
                //var ID = Convert.ToInt32(id);
                var item = db.Accounts.Find(id);
                if (item != null)
                {
                    db.Accounts.Remove(item);
                    db.SaveChanges();
                }
                var item2 = db.Users.Where(x => x.Account_ID == id).FirstOrDefault();
                if (item2 != null)
                {
                    db.Users.Remove(item2);
                    db.SaveChanges();
                }
                

                return RedirectToAction("Index", "User");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
    }
}