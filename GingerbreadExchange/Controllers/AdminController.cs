using GingerbreadExchange.Models;
using GingerbreadExchange.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace GingerbreadExchange.Controllers
{
    public class AdminController : Controller
    {
        API api = new API();

        public ActionResult Admin()
        {
            var histories = api.GetUnconfirmedHistoriesModel().Select(t => new HistoryVM(t)).ToList();

            return View(new IndexVM() { HistoryVMList = histories });
        }

        [HttpGet]
        public ActionResult ConfirmDeal(long histId, bool boo)
        {
            var tempTM = new TaskManager(histId, boo);

            Thread myThread = new Thread(new ThreadStart(tempTM.ConfirmDeal));
            myThread.Start();

            return Redirect("/Home/Index");
        }
    }
}