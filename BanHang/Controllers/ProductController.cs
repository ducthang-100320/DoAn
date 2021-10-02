using BanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace BanHang.Controllers
{
    public class ProductController : Controller
    {
        CECMSDbContext db = new CECMSDbContext();
        // GET: Product
        public ActionResult MobilePhone(int? page)
        {
            db = new CECMSDbContext();
            var Hang = db.Manufacturers.Where(x => x.CategoryID == 1 && x.Status == 1).ToList();
            Session["Hang"] = Hang;
            //  Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy theo ID mới có thể phân trang.
            var mobilephone = (from l in db.Products
                        select l).OrderBy(x => x.ID).Where(x => x.MenuID ==1);
            // 2. Gán số lượng dòng hiển thị
            int pageSize = 5;
            // 3. Nếu giá trị page truyền vào khác null thì gán pageNumber = pagengược lại pageNumber = 1
            int pageNumber = 1;
            if (page != null)
            {
                pageNumber = Convert.ToUInt16(page);
            }
            // 4. Trả về các dữ liệu được phân trang theo kích thước và số trang
            return View(mobilephone.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult MobilePhone2(int? page)
        {
            db = new CECMSDbContext();
            var Hang = db.Manufacturers.Where(x => x.CategoryID == 1 && x.Status == 1).ToList();
            Session["Hang"] = Hang;
            //  Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy theo ID mới có thể phân trang.
            var model = (from a in db.Products
                         join b in db.ProductPrices
                         on a.ID equals b.ProductID
                         where a.MenuID == 1
                         //group by a.ID
                         select new
                         {
                             ID = a.ID,
                             Image = a.Image,
                             Name = a.Name,
                             Price = b.Price,
                             Capacity = b.Capacity,
                             Color = b.Color,
                             PromotionPrice = b.PromotionPrice

                         }).ToList();
            List<ProductView> lstSaleProduct = new List<ProductView>();
            if (model != null)
            {
                foreach (var item in model)
                {
                    ProductView _ProductView = new ProductView();
                    _ProductView._Product = new Product();
                    _ProductView._ProductPrice = new ProductPrice();

                    _ProductView._Product.ID = item.ID;
                    _ProductView._Product.Image = item.Image;
                    _ProductView._Product.Name = item.Name;


                    _ProductView._ProductPrice.Price = item.Price;
                    _ProductView._ProductPrice.PromotionPrice = item.PromotionPrice;
                    _ProductView._ProductPrice.Capacity = item.Capacity;
                    _ProductView._ProductPrice.Color = item.Color;

                    lstSaleProduct.Add(_ProductView);

                }
            }
            return PartialView(lstSaleProduct);
            //// 2. Gán số lượng dòng hiển thị
            //int pageSize = 5;
            //// 3. Nếu giá trị page truyền vào khác null thì gán pageNumber = pagengược lại pageNumber = 1
            //int pageNumber = 1;
            //if (page != null)
            //{
            //    pageNumber = Convert.ToUInt16(page);
            //}
            //// 4. Trả về các dữ liệu được phân trang theo kích thước và số trang
            //return View(model.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Laptop(int? page)
        {
            var Hang = db.Manufacturers.Where(x => x.CategoryID == 2 && x.Status == 1).ToList();
            Session["Hang"] = Hang;
            //  Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy theo ID mới có thể phân trang.
            var mobilephone = (from l in db.Products
                               select l).OrderBy(x => x.ID).Where(x => x.MenuID == 2);
            // 2. Gán số lượng dòng hiển thị
            int pageSize = 5;
            // 3. Nếu giá trị page truyền vào khác null thì gán pageNumber = pagengược lại pageNumber = 1
            int pageNumber = 1;
            if (page != null)
            {
                pageNumber = Convert.ToUInt16(page);
            }
            // 4. Trả về các dữ liệu được phân trang theo kích thước và số trang
            return View(mobilephone.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Tablet(int? page)
        {
            var Hang = db.Manufacturers.Where(x => x.CategoryID == 3 && x.Status == 1).ToList();
            Session["Hang"] = Hang;
            var model = db.Products.Where(x => x.MenuID == 3).ToList();
            return View(model);
        }
        public ActionResult PhuKien()
        {
            var Hang = db.Manufacturers.Where(x => x.CategoryID == 4 && x.Status == 1).ToList();
            Session["Hang"] = Hang;
            var model = db.Products.Where(x => x.MenuID == 4).ToList();
            return View(model);
        }
        public ActionResult DongHoThongMinh()
        {
            var Hang = db.Manufacturers.Where(x => x.CategoryID == 5 && x.Status == 1).ToList();
            Session["Hang"] = Hang;
            var model = db.Products.Where(x => x.MenuID == 5).ToList();
            return View(model);
        }
        public ActionResult DongHoThoiTrang()
        {
            var Hang = db.Manufacturers.Where(x => x.CategoryID == 5 && x.Status == 1).ToList();
            Session["Hang"] = Hang;
            var model = db.Products.Where(x => x.MenuID == 5).ToList();
            return View(model);
        }
        //SessionStorage
        public ActionResult Details(int id)
        {
            db = new CECMSDbContext();
            var Category = db.ProductCategories.Where(x => x.Status == 1).ToList();
            Session["Category"] = Category;
            db = new CECMSDbContext();
            var model = db.Products.Find(id);
            return View(model);
        }
        // End SessionStorage
        //LocalStorage
        public ActionResult Details2(int id)
        {
            db = new CECMSDbContext();
            var Category = db.ProductCategories.Where(x => x.Status == 1).ToList();
            Session["Category"] = Category;
            ProductView productView = new ProductView();
            ProductPrice productPrice = new ProductPrice();
            var model = db.Products.Find(id);
            var model2 = db.ProductPrices.Where(x => x.ProductID == id).FirstOrDefault();
            productView._Product = model;
            productView._ProductPrice = model2;
            return View(productView);
        }
        //End LocalStorage

    }
}