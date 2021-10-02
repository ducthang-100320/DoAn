using BanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        CECMSDbContext db;
        // GET: Admin/Order
        public ActionResult Index()
        { 
            CECMSDbContext db = new CECMSDbContext();
            var User = db.Accounts.Where(x => x.Status == 1).ToList();
            Session["User"] = User;
            var product = db.Products.Where(x => x.Status == 1).ToList();
            Session["Product"] = product;
            var model = (from a in db.Orders
                         join b in db.OrderDetails

                         on a.ID equals b.OrderID
                         select new
                         {
                             ID = a.ID,
                             CreatedDate = a.CreatedDate,
                             OrderAddress = a.OrderAddress,
                             OrderPhone = a.OrderPhone,
                             UserID = a.UserID,
                             Status = a.Status,
                             ProductID = b.ProductID,
                             Quantity = b.Quantity,
                             Price = b.Price


                         }
                        ).ToList();
            List<OrderProduct> lstOrder = new List<OrderProduct>();
            foreach (var item in model)
            {
                OrderProduct _OrderProduct = new OrderProduct();
                _OrderProduct._order = new Order();
                _OrderProduct._orderDetail = new OrderDetail();

                _OrderProduct._order.ID = item.ID;
                _OrderProduct._order.CreatedDate = item.CreatedDate;
                _OrderProduct._order.OrderAddress = item.OrderAddress;
                _OrderProduct._order.OrderPhone = item.OrderPhone;
                _OrderProduct._order.Status = item.Status;
                _OrderProduct._order.CreatedDate = item.CreatedDate;
                _OrderProduct._orderDetail.ProductID = item.ProductID;
                _OrderProduct._orderDetail.Quantity = item.Quantity;
                _OrderProduct._orderDetail.Price = item.Price;
                _OrderProduct._order.UserID = item.UserID;

                lstOrder.Add(_OrderProduct);

            }
            return View(lstOrder);
        }
    }
}