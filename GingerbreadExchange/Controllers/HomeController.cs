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

        //[HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Gingerbread> gingerbreads = db.Gingerbreads.ToList();
            IEnumerable<Order> orders = db.Orders.ToList();
            IEnumerable<History> histories = db.Histories.ToList();
            IEnumerable<OrderView> bov = db.Gingerbreads.Join(db.Orders.Where(t=>t.DealOperation == Deal.Buy) , g => g.Id, o => o.GingerbreadId, (g, o) => new OrderView // результат
            {
                Price = g.Price,
                Count = g.Count,
                Email = o.Email
            }).OrderByDescending(p => p.Price).ToList() ;
            IEnumerable<OrderView> sov = db.Gingerbreads.Join(db.Orders.Where(t => t.DealOperation == Deal.Sell), g => g.Id, o => o.GingerbreadId, (g, o) => new OrderView // результат
            {
                Price = g.Price,
                Count = g.Count,
                Email = o.Email
            }).OrderBy(p => p.Price).ToList();
            ViewBag.BuyOrderView = bov;
            ViewBag.SellOrderView = sov;
            //ViewBag.Gingerbreads = gingerbreads;
            ViewBag.Orders = orders;
            ViewBag.Histories = histories;

            //var a = db.Gingerbreads.Join(db.Orders, g => g.Id, o => o.GingerbreadId, (g, o) => new // результат
            //{
            //    Price = g.Price,
            //    Count = g.Count,
            //    Email = o.Email
            //});//OrderByDescending(p => p.Price);
            return View("Index");
        }


        [HttpGet]
        public ViewResult BuyOrder()
        {
            
            //Index();
            return View();
        }


        [HttpPost]
        public /*ActionResult*/ void BuyOrder(Gingerbread gb, Order ord)
        {
            db.Gingerbreads.Add(gb);
            ord.CreationTime = DateTime.Now;
            ord.DealOperation = Deal.Buy;
            //var a = db.Gingerbreads.First(t => t == gb);
            ord.Gingerbread = gb;
            db.Orders.Add(ord);
            db.SaveChanges();
            Index();
            //return View("Index");
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