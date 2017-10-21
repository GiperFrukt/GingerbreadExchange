using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GingerbreadExchange.Models;

namespace GingerbreadExchange.Controllers
{
    public class HomeController : Controller
    {
        ExchangeContext db = new ExchangeContext();

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Gingerbread> gingerbreads = db.Gingerbreads;
            IEnumerable<Order> orders = db.Orders;
            IEnumerable<History> histories = db.Histories;
            ViewBag.Gingerbreads = gingerbreads;
            ViewBag.Orders = orders;
            ViewBag.Histories = histories;
            return View();
        }

        [HttpPost]
        public void Order(Gingerbread gb, Order ord)
        {
            db.Gingerbreads.Add(gb);
            ord.CreationTime = DateTime.Now;
            ord.DealOperation = Deal.Buy;
            //var a = db.Gingerbreads.First(t => t == gb);
            ord.Gingerbread = gb;
            db.Orders.Add(ord);
            db.SaveChanges();
        }

        //[HttpPost]
        //public ActionResult MyAction(string product, string action)
        //{
        //    if (action == "add")
        //    {

        //    }
        //    else if (action == "delete")
        //    {

        //    }
        //    // остальной код метода
        //}
    }
}