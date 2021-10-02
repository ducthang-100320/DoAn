using BanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Controllers
{
    public class HomeController : Controller
    {
        CECMSDbContext db = new CECMSDbContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Header()
        {
            db = new CECMSDbContext();
            var model = db.Headers.Where(x => x.Status == 1).ToList();
            return PartialView(model);
        }
        public ActionResult MenuHeader()
        {
            db = new CECMSDbContext();
            var model = db.Menus.Where(x => x.Status == 1).OrderBy(x => x.OrderNumber).ToList();
            return PartialView(model);
        }
        public ActionResult Slider()
        {
            db = new CECMSDbContext();
            var model = db.Sliders.Where(x => x.Status == 1).OrderBy(x => x.OrderNumber).ToList();
            return PartialView(model);
        }
        public ActionResult Logo()
        {
            
            return PartialView();
        }
        public ActionResult SaleProduct()
        {
            var model = (from a in db.Products
                         join b in db.ProductPrices
                         on a.ID equals b.ProductID
                         select new
                         {
                             ID = a.ID,
                             Image = a.Image,
                             Name = a.Name,
                             Price = b.Price,
                             PromotionPrice = b.PromotionPrice

                         }).ToList();
            List<ProductView> lstSaleProduct = new List<ProductView>();
            if (model != null && model.Count > 0)
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

                    lstSaleProduct.Add(_ProductView);

                }
            }
            return PartialView(lstSaleProduct);
        }
    }
}