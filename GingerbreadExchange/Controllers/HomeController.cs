using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GingerbreadExchange.ViewModels;
using GingerbreadExchange.Models;
using GingerbreadExchange.Models.Services;

namespace GingerbreadExchange.Controllers
{
    public class HomeController : Controller
    {
        ExchangeContext db = new ExchangeContext();

        //[HttpGet]
        public ActionResult Index()
        {
            var gingerbreads = GingerbreadService.QueryGingerbreads();
            var orders = OrderService.QueryOrders() as List<Order>;
            var histories = HistoryService.QueryHistories();

            var buyOrderVMList = new List<OrderVM>();
            (orders.Where(t => t.DealOperation == Deal.Buy).OrderByDescending(p => p.Gingerbread.Price)).ToList().ForEach(t =>
            {
                buyOrderVMList.Add(new OrderVM() { Count = t.Gingerbread.Count, Email = t.Email, Price = t.Gingerbread.Price });
            });

            var sellOrderVMList = new List<OrderVM>();
            (orders.Where(t => t.DealOperation == Deal.Sell).OrderBy(p => p.Gingerbread.Price)).ToList().ForEach(t =>
            {
                sellOrderVMList.Add(new OrderVM() { Count = t.Gingerbread.Count, Email = t.Email, Price = t.Gingerbread.Price });
            });
            
            var historyOrderVMList = new List<HistoryVM>();
            var indexVM = new IndexVM() { BuyVMList = buyOrderVMList, SellVMList = sellOrderVMList, HistoryVMList = historyOrderVMList };
            return View(indexVM);
        }

        [HttpPost]
        public ActionResult BuyOrder(string dealOperation, Gingerbread gb, string email)
        {
            var dealOp = (Deal)Enum.Parse(typeof(Deal), dealOperation);
            var order = new Order(dealOp, gb, email);
            OrderService.AddOrder(order);
            ExecuteOrder(order, gb);
            return Redirect("Index");
        }

        void ExecuteOrder(Order ord, Gingerbread gb)
        {
            ////если покупаем
            //IEnumerable<OrderVM> bov = db.Gingerbreads.Join(db.Orders.Where(t => t.DealOperation == Deal.Sell), g => g.Id, o => o.GingerbreadId, (g, o) => new OrderVM // результат
            //{
            //    Price = g.Price,
            //    Count = g.Count,
            //    Email = o.Email
            //}).OrderBy(p => p.Price).ToList();
            //var selected = bov.Where(t => t.Price <= gb.Price) as List<OrderVM>;
            //bool done = false;
            //int i = 0;
            //do
            //{
            //    var sOrder = selected.ElementAt(i);
            //    switch (ord.Gingerbread.Count < selected.Count)
            //    {
            //        case true:
            //            // TODO извлечь из БД не продукт, а сделку с этим продуктом !!!!!!!! исправить дату создания заявки на покупку и имэйл продавца
            //            var completedDeal = new History { DealTime = DateTime.Now, BuyOrderTime = ord.CreationTime, SellOrderTime = DateTime.Now,
            //                Price = ord.Gingerbread.Price, Count = ord.Gingerbread.Count, BuyEmail = ord.Email, SellEmail = ord.Email};
            //            db.Histories.Add(completedDeal);
            //            db.Orders.Remove(ord); // удаляем из бд ордер, продукт должен удалиться автоматически
            //            if (ord.Gingerbread.Count == selected.Count)
            //            {
            //                //db.Orders.Remove(db.Orders.Where(t => t.GingerbreadId == sOrde));
            //            }
                        
            //            break;
            //        case false:
            //            break;
            //    }
            //} while (!done);

            //db.SaveChanges();
            //foreach (var a in selected)
            //{

            //}
        }
        
    }
}