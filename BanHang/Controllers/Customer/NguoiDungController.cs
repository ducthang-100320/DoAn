using BanHang.Models;
using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BanHang.Controllers.Customer
{
    public class NguoiDungController : Controller
    {
        CECMSDbContext db = new CECMSDbContext();
        public ActionResult Index()
        {
            var model = db.Accounts.Where(x => x.Status == 1).FirstOrDefault();
            Session["NameUser"] = model;
            return View();
        }
        [HttpPost]
        public ActionResult Index(Account model)
        {

            return View();
        }
        // GET: Customer/NguoiDung
        //public ActionResult Register()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Register(AccountUser model, FormCollection collection)
        //{

        //    var item = db.Accounts.Where(x => x.AccountName == model.AccountName && x.Status == 1).FirstOrDefault();
        //    var item2 = db.Users.Where(x => x.Email == model.Email && x.Status == 1).FirstOrDefault();

        //    if (item != null)
        //    {
        //        ViewBag.ErrorAccountName = "Tên đăng nhập đã tồn tại";
        //    }
        //    else if (item2 != null)
        //    {
        //        ViewBag.ErrorEmail = "Email đã tồn tại";
        //    }
        //    else
        //    {
        //        var account = new Account();
        //        account.AccountName = model.AccountName;
        //        account.Password = model.Password;
        //        account.ConfirmPassword = model.ConfirmPassword;
        //        account.DateIssued = model.DateIssued = DateTime.Now;
        //        account.Status = model.Status = 1;
        //        account.CreatedDate = model.CreatedDate = DateTime.Now;
        //        account.ModifiedDate = model.ModifiedDate = DateTime.Now;
        //        db.Accounts.Add(account);
        //        db.SaveChanges();
        //        var user = new User();
        //        user.Account_ID = account.ID;
        //        user.UserName = model.UserName;
        //        user.Birthday = model.Birthday = DateTime.Now;
        //        user.Sex = model.Sex = 1;
        //        user.Address = model.Address;
        //        user.Phone = model.Phone;
        //        user.Email = model.Email;
        //        user.Description = model.Description = "ducthang";
        //        user.Status = model.Status = 1;
        //        user.CreatedDate = model.CreatedDate = DateTime.Now;
        //        user.ModifiedDate = model.ModifiedDate = DateTime.Now;
        //        db.Users.Add(user);

        //        var result = db.SaveChanges();
        //        if (result > 0)
        //        {
        //            ViewBag.Success = "Đăng ký thành công";
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Đăng ký thất bại");
        //        }

        //    }

        //    return View(model);

        //}
        [HttpPost]
        public JsonResult Login(string AccountName, string Password)
        {
            var db = new CECMSDbContext();
            var NameUser = db.Accounts.Where(x => x.AccountName == AccountName && x.Password == Password).FirstOrDefault();
            if(NameUser != null)
            {
                Session["NameUser"] = NameUser;
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Session["NameUser"] = null;
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            
        }
        
        //Login with Google API
        [HttpPost]
        public JsonResult ReturnURL(string Email, string LastName, string FirstName, string GoogleID, string ProfileURL)
        {
            //Account acc = new Account();
            Session["GmailAPI"] = FirstName + LastName;
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        //End login with Google API

        public ActionResult Logout()
        {
            CECMSDbContext db = new CECMSDbContext();
            var NameUser = db.Accounts.FirstOrDefault();
            Session["NameUser"] = NameUser;
            Session.Remove("NameUser");
            Session.Remove("GmailAPI");
            return RedirectToAction("Index", "Home");
        }
        //Login with Facebook API

        private Uri RediredtUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        [AllowAnonymous]
        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "3188508588142664",
                client_secret = "707b46c1cc05f7af2a6f9ef0a17c37a5",
                redirect_uri = RediredtUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }
        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = "3188508588142664",
                client_secret = "707b46c1cc05f7af2a6f9ef0a17c37a5",
                redirect_uri = RediredtUri.AbsoluteUri,
                code = code
            });
            var accessToken = result.access_token;
            Session["AccessToken"] = accessToken;
            fb.AccessToken = accessToken;
            dynamic me = fb.Get("me?fields=link,first_name,currency,last_name,email,gender,locale,timezone,verified,picture,age_range");
            string email = me.email;
            TempData["email"] = me.email;
            TempData["first_name"] = me.first_name;
            TempData["lastname"] = me.last_name;
            TempData["picture"] = me.picture.data.url;
            //Session["FacebookAPI"] = TempData["lastname"] TempData["first_name"];
            FormsAuthentication.SetAuthCookie(email, false);
            return RedirectToAction("Index", "Home", new { area = "" });
        }
        // End Login with Facebook API
        [HttpGet]
        public ActionResult profile(int id)
        {
            CECMSDbContext db = new CECMSDbContext();
            var item1 = db.Users.Find(id);
            var item2 = db.Accounts.Find(item1.Account_ID);
            AccountUser model = new AccountUser();
            model._Account.ID = item1.ID;
            model._Account.AccountName = item2.AccountName;
            //model.Account_ID = item2.ID;
            model._User.Address = item1.Address;
            model._User.Email = item1.Email;
            model._User.Phone = item1.Phone;
            model._User.Birthday = item1.Birthday;
            //model.CreatedDate = DateTime.Now;
            //model.CreatedUser = item1.CreatedUser;
            //model.DateIssued = item2.DateIssued;
            //model.Description = item1.Description;
            //model.ModifiedDate = item2.ModifiedDate;
            //model.ModifiedUser = item2.ModifiedUser;
            model._Account.Password = item2.Password;
            //var Sex = Collection["customRadio"];
            //if (collection["customRadio"].ToString().Trim() != "")
            //    userItem.Sex = Convert.ToInt32(collection["cboStatus"].ToString());
            //else
            //    userItem.Sex = 0;
            model._User.Sex = item1.Sex;
            model._User.UserName = item1.UserName;
            model._User.Image = item1.Image;
            return View(model);
        }

        //[HttpPost]
        //public ActionResult profile(AccountUser item, HttpPostedFile filePost, FormCollection collection, int id)
        //{


        //    CECMSDbContext db = new CECMSDbContext();

        //    var NameUser = db.Accounts.Where(x => x.AccountName == item.AccountName && x.Status == 1).FirstOrDefault();
        //    Session["UserName"] = NameUser;
        //    var acc = db.Accounts.Find(item.Account_ID);

        //    acc.AccountName = item.AccountName;
        //    //var uResult = db.SaveChanges();
        //    var user = db.Users.Where(x => x.Account_ID == item.Account_ID).SingleOrDefault();
        //    user.UserName = item.UserName;
        //    user.Birthday = item.Birthday;
        //    user.ModifiedUser = "anhpd";
        //    var Sex = collection["customRadio"];
        //    if (collection["customRadio"].ToString().Trim() != "Nam")
        //        item.Sex = Convert.ToInt32(collection["customRadio"].ToString());
        //    else
        //        item.Sex = 0;
        //    //user.Sex = item.Sex;
        //    user.Status = 1;
        //    user.Address = item.Address;
        //    string filelocation = "";
        //    if (Request.Files["filePost"].ContentLength > 0)
        //    {
        //        string fileExtenSion = System.IO.Path.GetExtension(Request.Files["filePost"].FileName);
        //        filelocation = Server.MapPath("~/Content/") + Request.Files["filePost"].FileName;

        //        if (System.IO.File.Exists(filelocation))
        //        {
        //            System.IO.File.Exists(filelocation);
        //        }
        //        Request.Files["filePost"].SaveAs(filelocation);
        //        int icontent = filelocation.IndexOf("Content");
        //        if (icontent > 0)
        //        {
        //            user.Image = @"\" + filelocation.Substring(icontent, filelocation.Length - icontent);
        //        }
        //        else
        //        {
        //            user.Image = item.Image;
        //        }


        //        var uResult = db.SaveChanges();
        //        if (uResult >= 0)
        //        {
        //            ViewBag.Success = "Cập nhật không thành công";
        //            return RedirectToAction("Index", "Home", new { area = "", id = item.ID });

        //        }
        //        else
        //        {
        //            ViewBag.Error = "Cập nhật không thành công";
        //            //SetAlert()
        //            return View();
        //        }
        //    }
        //    return View();
        //}
        public ActionResult Payment()
        {

            return View();
        }
        public ActionResult Order()
        {
            ViewBag.Breadcrumb = "Đơn hàng của bạn";
            return View();
        }



    }
}