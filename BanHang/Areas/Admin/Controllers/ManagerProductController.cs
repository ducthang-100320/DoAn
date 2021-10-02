using BanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Areas.Admin.Controllers
{
    public class ManagerProductController : Controller
    {
        CECMSDbContext db = new CECMSDbContext();
        // GET: Admin/ManagerProduct
        public ActionResult Index()
        {
            ViewBag.Breadcrumb = "Danh mục sản phẩm";
            db = new CECMSDbContext();
            var MenuProduct = db.Menus.ToList();
            Session["MenuProduct"] = MenuProduct;
            var Hang = db.Manufacturers.ToList();
            Session["Hang"] = Hang;
            var model = (from a in db.Products
                         join b in db.ProductPrices

                         on a.ID equals b.ProductID

                         select new
                         {
                             ID = a.ID,
                             Code = a.Code,
                             Image = a.Image,
                             Name = a.Name,
                             Price = b.Price,
                             PromotionPrice = b.PromotionPrice,
                             Color = b.Color,
                             Capacity = b.Capacity,
                             ProductID = b.ProductID,
                             MenuID = a.MenuID,
                             Warranty = a.Warranty,
                             Description = a.Description,
                             ManufacturerID = a.ManufacturerID

                         }
                        ).ToList();
            List<ProductView> lstProduct = new List<ProductView>();
            foreach (var item in model)
            {
                ProductView _lstProduct = new ProductView();
                _lstProduct._Product = new Product();
                _lstProduct._ProductPrice = new ProductPrice();

                _lstProduct._Product.ID = item.ID;
                _lstProduct._Product.Code = item.Code;
                _lstProduct._Product.Name = item.Name;
                _lstProduct._Product.Image = item.Image;
                _lstProduct._Product.MenuID = item.MenuID;
                _lstProduct._Product.ManufacturerID = item.ManufacturerID;
                _lstProduct._Product.Warranty = item.Warranty;
                _lstProduct._Product.Description = item.Description;
                _lstProduct._ProductPrice.Price = item.Price;
                _lstProduct._ProductPrice.PromotionPrice = item.PromotionPrice;
                _lstProduct._ProductPrice.Color = item.Color;
                _lstProduct._ProductPrice.Capacity = item.Capacity;
                _lstProduct._ProductPrice.ProductID = item.ProductID;

                lstProduct.Add(_lstProduct);

            }
            return View(lstProduct);
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            db = new CECMSDbContext();
            var MenuProduct = db.Menus.ToList();
            Session["MenuProduct"] = MenuProduct;
            var Hang = db.Manufacturers.ToList();
            Session["Hang"] = Hang;
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(ProductView model, FormCollection collection, HttpPostedFile filePost, HttpPostedFile filePost2)
        {
            var NameUser = Session["NameUser"] as BanHang.Models.Account;


            CECMSDbContext db = new CECMSDbContext();
            string fileLocation = "";
            if (Request.Files["filePost"].ContentLength <= 0) { model._Product.Image = ""; }
            //model.Image = "../assets/images/user_pic.jfif";
            ModelState["filePost"].Errors.Clear();
            if (ModelState.IsValid == true)
            {
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

                Product product = new Product();

                //var itemProduct = db.Products.Where(x => x.ID == model._Product.ID).ToList();

                product.Code = model._Product.Code;
                product.Name = model._Product.Name;
                product.MenuID = Convert.ToInt32(collection["cboLoai"].ToString());
                product.ManufacturerID = Convert.ToInt32(collection["cboHang"].ToString());
                product.Warranty = model._Product.Warranty;
                product.Installment = model._Product.Installment;
                product.Description = model._Product.Description;
                product.MetaKeywords = model._Product.MetaKeywords;
                product.Status = 1;
                product.CreatedUser = NameUser.AccountName;
                product.CreatedDate = DateTime.Now;
                product.ModifiedUser = NameUser.AccountName;
                product.ModifiedDate = DateTime.Now;
                db.Products.Add(product);
                db.SaveChanges();
                ProductPrice productPrice = new ProductPrice();
                var itemProductPrice = db.ProductPrices.Where(x => x.ProductID == model._Product.ID).ToList();

                productPrice.ProductID = product.ID;
                productPrice.Price = model._ProductPrice.Price;
                productPrice.PromotionPrice = model._ProductPrice.PromotionPrice;
                productPrice.Capacity = model._ProductPrice.Capacity;
                productPrice.Color = model._ProductPrice.Color;
                productPrice.DateIssued = model._ProductPrice.DateIssued;
                productPrice.IsDate = model._ProductPrice.IsDate;
                productPrice.ID = -1;
                int iContent = fileLocation.IndexOf("Content");
                if (iContent > 0)
                {
                    product.Image = @"\" + fileLocation.Substring(iContent, fileLocation.Length - iContent);
                }


                db.ProductPrices.Add(productPrice);
                db.SaveChanges();
                return RedirectToAction("Index", "ManagerProduct");
            }
            else
            {
                ModelState.AddModelError("", "Chưa nhập đầy đủ thông tin");
            }
            return View();

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            db = new CECMSDbContext();
            var MenuProduct = db.Menus.ToList();
            Session["MenuProduct"] = MenuProduct;
            var Hang = db.Manufacturers.ToList();
            Session["Hang"] = Hang;

            var model = (from a in db.Products
                         join b in db.ProductPrices

                         on a.ID equals b.ProductID
                         where a.ID == id
                         select new
                         {
                             ID = a.ID,
                             Code = a.Code,
                             Image = a.Image,
                             Name = a.Name,
                             Price = b.Price,
                             Installment = a.Installment,
                             AccompanyingProducts = a.AccompanyingProducts,
                             MetaTitle = a.MetaTitle,
                             MetaKeywords = a.MetaKeywords,
                             Status = a.Status,
                             PromotionPrice = b.PromotionPrice,
                             Color = b.Color,
                             Capacity = b.Capacity,
                             ProductID = b.ProductID,
                             MenuID = a.MenuID,
                             Warranty = a.Warranty,
                             Description = a.Description,
                             ManufacturerID = a.ManufacturerID

                         }
                        ).FirstOrDefault();

            
                ProductView _lstProduct = new ProductView();
                _lstProduct._Product = new Product();
                _lstProduct._ProductPrice = new ProductPrice();

                _lstProduct._Product.ID = model.ID;
                _lstProduct._Product.Code = model.Code;
                _lstProduct._Product.Name = model.Name;
                _lstProduct._Product.Image = model.Image;
                _lstProduct._Product.MenuID = model.MenuID;
                _lstProduct._Product.ManufacturerID = model.ManufacturerID;
                _lstProduct._Product.MetaTitle = model.MetaTitle;
                _lstProduct._Product.MetaKeywords = model.MetaKeywords;
                _lstProduct._Product.Installment = model.Installment;
                _lstProduct._Product.AccompanyingProducts = model.AccompanyingProducts;
                _lstProduct._Product.Status = model.Status;
                _lstProduct._Product.Warranty = model.Warranty;
                _lstProduct._Product.Description = model.Description;
                _lstProduct._ProductPrice.Price = model.Price;
                _lstProduct._ProductPrice.PromotionPrice = model.PromotionPrice;
                _lstProduct._ProductPrice.Color = model.Color;
                _lstProduct._ProductPrice.Capacity = model.Capacity;
                _lstProduct._ProductPrice.ProductID = model.ProductID;

                

            
            return View(_lstProduct);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(ProductView model, FormCollection collection, HttpPostedFile filePost, HttpPostedFile filePost2)
        {

            db = new CECMSDbContext();
            var item = db.Products.Find(model._Product.ID);
            var products = db.Products.Where(x => x.Name.Equals(model._Product.Name)).FirstOrDefault();
            if (products != null)
            {
                ViewBag.ErrorEdit = "Sản phẩm này đã tồn tại trong hệ thống";
                //return RedirectToAction("Edit", "ManagerProduct");
                return View(model);
            }
            item.Name = model._Product.Name;
            //item.Price = model._ProductPrice.Price;
            //item.PromotionPrice = model._ProductPrice.PromotionPrice;
            //if (model._ProductPrice.Price <= model._ProductPrice.PromotionPrice)
            //{
            //    ViewBag.Price = "Giá khuyến mại phải nhỏ hơn giá sản phẩm ";
            //    return View(model);
            //}
            //if (model._ProductPrice.Price <= 0 || model._ProductPrice.PromotionPrice <= 0)
            //{
            //    ViewBag.Price2 = "Giá sản phẩm phải lớn hơn 0";
            //    return View(model);
            //}
            item.CreatedUser = model._Product.CreatedUser;
            item.ModifiedUser = model._Product.ModifiedUser;
            model._Product.Status = Convert.ToInt32(collection["cboStatus"].ToString());

            if (collection["cboLoai"].ToString().Trim() != "")
                item.MenuID = Convert.ToInt32(collection["cboLoai"].ToString());
            else
                item.MenuID = -1;

            if (collection["cboHang"].ToString().Trim() != "")
                item.ManufacturerID = Convert.ToInt32(collection["cboHang"].ToString());
            else
                item.ManufacturerID = -1;

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
                    item.Image = model._Product.Image;
                }
            }
            var itemPrice = db.ProductPrices.Find(model._ProductPrice.ProductID);
            itemPrice.Price = model._ProductPrice.Price;
            itemPrice.PromotionPrice = model._ProductPrice.PromotionPrice;
            itemPrice.Color = model._ProductPrice.Color;
            itemPrice.Capacity = model._ProductPrice.Capacity;
            itemPrice.DateIssued = model._ProductPrice.DateIssued;
            itemPrice.IsDate = model._ProductPrice.IsDate;

            //string strRadio = collection["product"].ToString();
            //if (strRadio == null)
            //{
            //    model._Product.IsHot = 0;
            //}

            //if (strRadio == "isHot")
            //{
            //    model._Product.IsHot = 1;
            //}
            //else if (strRadio == "isNew")
            //{
            //    model._Product.IsHot = 0;
            //}
            //else if (strRadio == "isSale")
            //{
            //    model._Product.IsHot = 0;
            //}

            item.Description = model._Product.Description;
            var Result = db.SaveChanges();

            if (Result >= 0)
            {
                ViewBag.SuccessEdit = "Cập nhật thông tin thành công";
                return View(model);
                //return RedirectToAction("Edit", "ManagerProduct");
                //return RedirectToAction("Index", "ManagerProduct");
            }
            else
            {
                ModelState.AddModelError("error", "Lỗi trong quá trình ghi dữ liệu");
                return View();
            }
        }
        public ActionResult Details(int id)
        {
            db = new CECMSDbContext();
            var Category = db.ProductCategories.Where(x => x.Status == 1).ToList();
            Session["Category"] = Category;
            db = new CECMSDbContext();
            var model = db.Products.Find(id);
            return View(model);
        }
        public ActionResult Delete(int id)
            {
                try
                {
                    db = new CECMSDbContext();
                    var item = db.Products.Find(id);
                    if (item != null)
                    {
                        db.Products.Remove(item);
                        db.SaveChanges();
                    }
                    var item2 = db.ProductPrices.Where(x => x.ProductID == id).FirstOrDefault();
                    if (item2 != null)
                    {
                        db.ProductPrices.Remove(item2);
                        db.SaveChanges();
                    }

                return RedirectToAction("Index", "ManagerProduct");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View();
                }
            }
    }
}