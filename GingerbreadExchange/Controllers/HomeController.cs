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
            var histories = HistoryService.QueryHistories() as List<History>;

            var buyOrderVMList = new List<OrderVM>();
            (orders.Where(t => t.DealOperation == Deal.Buy).OrderByDescending(p => p.Gingerbread.Price)).ToList()
                .ForEach(t => buyOrderVMList.Add(new OrderVM(t)));

            var sellOrderVMList = new List<OrderVM>();
            (orders.Where(t => t.DealOperation == Deal.Sell).OrderBy(p => p.Gingerbread.Price)).ToList()
                .ForEach(t =>sellOrderVMList.Add(new OrderVM(t)));
            
            var historyOrderVMList = new List<HistoryVM>();
            histories.ToList().ForEach(t => historyOrderVMList.Add(new HistoryVM(t)));


            var indexVM = new IndexVM() { BuyVMList = buyOrderVMList, SellVMList = sellOrderVMList, HistoryVMList = historyOrderVMList };
            return View(indexVM);
        }

        [HttpPost]
        public ActionResult BuyOrder(string dealOperation, Gingerbread gb, string email)
        {
            var dealOp = (Deal)Enum.Parse(typeof(Deal), dealOperation);
            var order = new Order(dealOp, gb, email);
            OrderService.AddOrder(order);
            ExecuteOrder(order);
            return Redirect("Index");
        }

        void ExecuteOrder(Order buy)
        {
            
            //если покупаем
            var orders = OrderService.QueryOrders() as List<Order>;
            var o = orders.Where(t => t.DealOperation == Deal.Buy).OrderByDescending(p => p.Gingerbread.Price).ToList();

            // выбрали тех, у кого можем купить, результаты отсортировали по возрастанию цены
            var selected = o.Where(t => t.Gingerbread.Price <= buy.Gingerbread.Price).ToList();
            bool done = false;
            int i = 0;
            if (selected != null)
            {
                do
                {
                    var sell = selected.ElementAt(i);
                    switch (buy.Gingerbread.Count == sell.Gingerbread.Count)
                    {
                        case true:

                            var completedDeal = new History(buy, sell);
                            HistoryService.AddHistory(completedDeal);
                            OrderService.DeleteOrder(buy);
                            OrderService.DeleteOrder(sell);
                            //if (buy.Gingerbread.Count == selected.Count)
                            //{
                            //    //db.Orders.Remove(db.Orders.Where(t => t.GingerbreadId == sOrde));
                            //}

                            break;
                        case false:
                            break;
                    }
                } while (!done);
            }
            foreach (var a in selected)
            {

            }
        }
        
    }
}