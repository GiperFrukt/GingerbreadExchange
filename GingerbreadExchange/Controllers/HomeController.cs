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

        public ActionResult Index()
        {
            IEnumerable<Gingerbread> gingerbreads = db.Gingerbreads;
            ViewBag.Gingerbreads = gingerbreads;
            return View();
        }
    }
}