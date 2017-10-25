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
                .ForEach(t => sellOrderVMList.Add(new OrderVM(t)));

            var historyOrderVMList = new List<HistoryVM>();
            histories.ToList().ForEach(t => historyOrderVMList.Add(new HistoryVM(t)));


            var indexVM = new IndexVM() { BuyVMList = buyOrderVMList, SellVMList = sellOrderVMList, HistoryVMList = historyOrderVMList };
            return View(indexVM);
        }

        [HttpPost]
        public ActionResult Order([Bind(Include = "GingerbreadVM, OrderVM")] IndexVM index, string dealOperation)
        {
            if (ModelState.IsValid)
            {
                index.OrderVM.DealOperation = dealOperation == Deal.Buy.ToString() ? Deal.Buy : Deal.Sell;
                var gingerbread = new Gingerbread(count: index.GingerbreadVM.Count, price: index.GingerbreadVM.Price);
                var order = new Order(email: index.OrderVM.Email, dealOperation: index.OrderVM.DealOperation, gingerbread: gingerbread);
                OrderService.AddOrder(order);
                
                ExecuteOrder(order);
            }

            return Redirect("Index");
        }

        void ExecuteOrder(Order ord)
        {
            if (ord.DealOperation == Deal.Buy)
            {
                ExecuteBuyOrder(ord);
            }
            else //если продаём
            {
                ExecuteSellOrder(ord);
            }
        }

        void ExecuteBuyOrder(Order buyOrd)
        {
            var orders = OrderService.QueryOrders() as List<Order>;

            var temp = orders.Where(t => t.DealOperation == Deal.Sell).OrderBy(p => p.Gingerbread.Price).ToList();
            // выбрали тех, у кого можем купить, результаты отсортировали по возрастанию цены
            var selected = temp.Where(t => t.Gingerbread.Price <= buyOrd.Gingerbread.Price).ToList();


            if (selected.Count != 0)
            {
                bool done = false;
                int i = 0;
                do
                {
                    var sellOrd = selected.ElementAt(i);
                    var completedDeal = new History(buyOrd, sellOrd, sellOrd.Gingerbread.Price);
                    HistoryService.AddHistory(completedDeal);

                    if (buyOrd.Gingerbread.Count == sellOrd.Gingerbread.Count)
                    {
                        OrderService.DeleteOrder(buyOrd);
                        OrderService.DeleteOrder(sellOrd);
                        done = true;
                    }
                    else if (buyOrd.Gingerbread.Count < sellOrd.Gingerbread.Count)
                    {
                        sellOrd.Gingerbread.Count = sellOrd.Gingerbread.Count - buyOrd.Gingerbread.Count;
                        GingerbreadService.UpdateGingerbread(sellOrd.Gingerbread);
                        OrderService.DeleteOrder(buyOrd);
                        done = true;
                    }
                    else // if (buy.Gingerbread.Count > sell.Gingerbread.Count)
                    {
                        buyOrd.Gingerbread.Count = buyOrd.Gingerbread.Count - sellOrd.Gingerbread.Count;
                        GingerbreadService.UpdateGingerbread(buyOrd.Gingerbread);
                        OrderService.DeleteOrder(sellOrd);
                    }
                    i++;
                } while (selected.Count > i && !done);
            }
        }

        void ExecuteSellOrder(Order sellOrd)
        {
            var orders = OrderService.QueryOrders() as List<Order>;

            var temp = orders.Where(t => t.DealOperation == Deal.Buy).OrderByDescending(p => p.Gingerbread.Price).ToList();
            // выбрали тех, уому можем продать, результаты отсортировали по убыванию цены
            var selected = temp.Where(t => t.Gingerbread.Price >= sellOrd.Gingerbread.Price).ToList();


            if (selected.Count != 0)
            {
                bool done = false;
                int i = 0;
                do
                {
                    var bOrd = selected.ElementAt(i);
                    var completedDeal = new History(bOrd, sellOrd, bOrd.Gingerbread.Price);
                    HistoryService.AddHistory(completedDeal);

                    if (sellOrd.Gingerbread.Count == bOrd.Gingerbread.Count)
                    {
                        OrderService.DeleteOrder(bOrd);
                        OrderService.DeleteOrder(sellOrd);
                        done = true;
                    }
                    else if (sellOrd.Gingerbread.Count < bOrd.Gingerbread.Count)
                    {
                        bOrd.Gingerbread.Count = bOrd.Gingerbread.Count - sellOrd.Gingerbread.Count;
                        GingerbreadService.UpdateGingerbread(bOrd.Gingerbread);
                        OrderService.DeleteOrder(sellOrd);
                        done = true;
                    }
                    else // if (sellOrd.Gingerbread.Count > bOrd.Gingerbread.Count)
                    {
                        sellOrd.Gingerbread.Count = sellOrd.Gingerbread.Count - bOrd.Gingerbread.Count;
                        GingerbreadService.UpdateGingerbread(sellOrd.Gingerbread);
                        OrderService.DeleteOrder(bOrd);
                    }
                    i++;
                } while (selected.Count > i && !done);
            }
        }

    }
}