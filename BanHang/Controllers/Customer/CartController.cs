using BanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Controllers.Customer
{
    public class CartController : Controller
    {
        CECMSDbContext db = new CECMSDbContext();
        // GET: Cart
        public ActionResult Index()
        {
            var nameUser = db.Accounts.Where(x => x.Status == 1).FirstOrDefault();
            Session["NameUser"] = nameUser;
            return View();
        }
        public ActionResult Index2()
        {
            db = new CECMSDbContext();
            //var Vourcher = db.Vouchers.Where(x => x.Status == 1).ToList();
            //Session["Vourcher"] = Vourcher;
            return View();
        }
        public ActionResult AddCart(int Id, string Image, string PromotionPrice , string Name, string Price)
        {
            decimal Total = 0;
            var lstItem = new List<CartItem>();
            Session["TotalProduct"] = 0;
            // Kiểm tra giỏ hàng có rổng không
            if (Session["Cart"] == null)
            {
                // Thêm mới sản phẩm
                var item = new CartItem();
                item.Id = Id;
                item.Name = Name;
                item.Image = Image;
                item.PromotionPrice = Convert.ToDecimal(PromotionPrice);
                item.Price = Convert.ToDecimal(Price);
                item.Quantity = 1;
                lstItem.Add(item);
                Session["Cart"] = lstItem;
                Total = item.PromotionPrice;
                Session["TotalProduct"] = lstItem.Count;
                Session["TotalMoney"] = Total.ToString("N0");
            }
            //Kiểm tra sản phẩm đã có trong giỏ hàng chưa
            else
            {
                lstItem = (List<CartItem>) Session["Cart"];
                bool IsCheck = false;
                foreach (var item in lstItem)
                {
                    if(item.Id == Id)
                    {
                        item.Quantity = item.Quantity + 1;
                        IsCheck = true;
                        break;
                    }
                    
                }
                if (IsCheck == false)
                {
                    var item = new CartItem();
                    item.Id = Id;
                    item.Name = Name;
                    item.Image = Image;
                    item.PromotionPrice = Convert.ToDecimal(PromotionPrice);
                    item.Price = Convert.ToDecimal(Price);
                    item.Quantity = 1;
                    lstItem.Add(item);
                }
                Session["Cart"] = lstItem;

                // Tính tổng tiền
                foreach(var item in lstItem)
                {
                    Total =Total +  item.PromotionPrice * item.Quantity;
                }
                Session["TotalProduct"] = lstItem.Count;
                Session["TotalMoney"] = Total.ToString("N0");
            }
            return RedirectToAction("Index","Cart");
        }
        public ActionResult RemoveCart(int Id)
        {
            decimal Total = 0;
            var lstItem = (List<CartItem>)Session["Cart"];
            lstItem.RemoveAll(x => x.Id == Id);
            Session["Cart"] = lstItem;
            foreach (var item in lstItem)
            {
                Total = Total + item.PromotionPrice * item.Quantity;
            }
            Session["TotalProduct"] = lstItem.Count;
            Session["TotalMoney"] = Total.ToString("N0");
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public JsonResult UpdateCart(int Id, int Quantity/*, string Price*/)
        {
            db = new CECMSDbContext();
            decimal Total = 0;
            Session["TotalProduct"] = 0;
            var lstItem = (List<CartItem>)Session["Cart"];
            Session["Cart"] = lstItem;
            
            foreach (var item in lstItem)
            {
                if (item.Id == Id)
                {
                    if (Quantity == 0)
                    {
                        lstItem.Remove(item);
                        Session["TotalProduct"] = lstItem.Count;
                        break;
                        
                        //return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    item.Quantity = Quantity;
                    //item.PromotionPrice = Convert.ToDecimal(Price);

                }
                Total = Total + item.PromotionPrice * item.Quantity;
                Session["TotalMoney"] = Total.ToString("N0");
                Session["TotalProduct"] = lstItem.Count;
                db.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Checkout(Checkout model)
        {
            
            Session["Reciver"] = model;
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckVourcher(string VourcherCode)
        {
            
            db = new CECMSDbContext();
            
            var item = db.Vouchers.Where(x => x.Status == 1).ToList();
            foreach (var item1 in item)
            {
                if (VourcherCode == item1.VoucherCode)
                {
                    Session["Vourcher"] = item1;
                    return Json(item1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Session["Vourcher"] = null;
                }
            }
            
                return Json(false, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        public JsonResult Order(List<CartItem> item)
        {
            if (Session["NameUser"] != null)
            {
                var db = new CECMSDbContext();
                var reciver = (BanHang.Models.Checkout)Session["Reciver"];
                var nameUser = (BanHang.Models.Account)Session["NameUser"];
                var model = new OrderProduct();
                model._order = new Order();
                model._orderDetail = new OrderDetail();
                model._order.OrderAddress = reciver.Address;
                model._order.UserID = nameUser.ID;
                model._order.OrderPhone = reciver.Phone;
                model._order.CreatedDate = DateTime.Now;
                model._order.Status = 0;
                db.Orders.Add(model._order);
                db.SaveChanges();
                foreach (var itemCart in item)
                {
                    model._orderDetail.OrderID = model._order.ID;
                    model._orderDetail.Price = itemCart.PromotionPrice;
                    model._orderDetail.ProductID = itemCart.Id;
                    model._orderDetail.Quantity = itemCart.Quantity;
                }
                db.OrderDetails.Add(model._orderDetail);
                db.SaveChanges();

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            
        }
        public ActionResult Submit()
        {
            return View();
        }
    }
}