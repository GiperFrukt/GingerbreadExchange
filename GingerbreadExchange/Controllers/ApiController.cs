using GingerbreadExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GingerbreadExchange.Controllers
{
    public class ApiController : Controller
    {
        API api = new API();

        [HttpGet]
        public JsonResult GetBuyOrders(string name)
        {
            var jsondata = api.GetBuyOrdersModel();
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSellOrders(string name)
        {
            var jsondata = api.GetSellOrdersModel();
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }

    }
}