using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GingerbreadExchange.ViewModels;
using GingerbreadExchange.Models;
using GingerbreadExchange.Models.Services;
using System.Threading;

namespace GingerbreadExchange.Controllers
{
    public class HomeController : Controller
    {
        API api = new API();

        public ActionResult Index(string cur)
        {
            if (HttpContext.Request.Cookies.AllKeys.Contains("Currency"))
            {
                if (HttpContext.Request.Cookies["Currency"].Value != cur)
                {
                    HttpContext.Response.Cookies["Currency"].Value = cur;
                }
            }
            else
            {
                HttpContext.Response.Cookies["Currency"].Value = "Rur";
            }
            var indexVM = GetCompositeViewModel();
            return View(indexVM);
        }

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

            return Redirect("Index");
        }


        [HttpPost]
        public ActionResult Index([Bind(Include = "GingerbreadVM, OrderVM, CurrentCurrency")] IndexVM index, string dealOperation)
        {
            if (ModelState.IsValid)
            {
                index.OrderVM.DealOperation = dealOperation == Deal.Buy.ToString() ? Deal.Buy : Deal.Sell;
                var gingerbread = new Gingerbread(count: index.GingerbreadVM.Count, price: index.GingerbreadVM.Price/*index.CurrentCurrency.AttitudeToRuble*/);
                var order = new Order(email: index.OrderVM.Email, dealOperation: index.OrderVM.DealOperation, gingerbread: gingerbread);
                //OrderService.AddOrder(order);
                api.Add(order);
                
                ExecuteOrder(order);
            }
            var compIndex = GetCompositeViewModel();
            return View(compIndex);
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
                    if (buyOrd.Gingerbread.Count == sellOrd.Gingerbread.Count)
                    {
                        buyOrd.OrderStatus = Status.OnConfirmation;
                        sellOrd.OrderStatus = Status.OnConfirmation;
                        var completedDeal = new History(buyOrd, sellOrd, sellOrd.Gingerbread.Price);
                        HistoryService.AddHistory(completedDeal);
                        done = true;
                    }
                    else if (buyOrd.Gingerbread.Count < sellOrd.Gingerbread.Count)
                    {
                        // заказ, уходящий на сделку
                        var residual = new Order(sellOrd, new Gingerbread(buyOrd.Gingerbread.Count, sellOrd.Gingerbread.Price));
                        OrderService.AddOrder(residual);

                        // осталось пряников у продаца
                        sellOrd.Gingerbread.Count = sellOrd.Gingerbread.Count - buyOrd.Gingerbread.Count;
                        GingerbreadService.UpdateGingerbread(sellOrd.Gingerbread);

                        buyOrd.OrderStatus = Status.OnConfirmation;
                        residual.OrderStatus = Status.OnConfirmation;
                        var completedDeal = new History(buyOrd, residual, sellOrd.Gingerbread.Price);
                        HistoryService.AddHistory(completedDeal);
                        done = true;
                    }
                    else // if (buy.Gingerbread.Count > sell.Gingerbread.Count)
                    {
                        // то, что уходит на сделку
                        var residual = new Order(buyOrd, new Gingerbread(sellOrd.Gingerbread.Count, buyOrd.Gingerbread.Price));
                        OrderService.AddOrder(residual);

                        // столько пряников не купили
                        buyOrd.Gingerbread.Count = buyOrd.Gingerbread.Count - sellOrd.Gingerbread.Count;
                        GingerbreadService.UpdateGingerbread(buyOrd.Gingerbread);
                        OrderService.UpdateOrder(buyOrd);

                        residual.OrderStatus = Status.OnConfirmation;
                        sellOrd.OrderStatus = Status.OnConfirmation;
                        var completedDeal = new History(residual, sellOrd, sellOrd.Gingerbread.Price);
                        HistoryService.AddHistory(completedDeal);
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
                    var buyOrd = selected.ElementAt(i);


                    if (sellOrd.Gingerbread.Count == buyOrd.Gingerbread.Count)
                    {
                        buyOrd.OrderStatus = Status.OnConfirmation;
                        sellOrd.OrderStatus = Status.OnConfirmation;
                        var completedDeal = new History(buyOrd, sellOrd, buyOrd.Gingerbread.Price);
                        HistoryService.AddHistory(completedDeal);
                        done = true;
                    }
                
                    else if (sellOrd.Gingerbread.Count < buyOrd.Gingerbread.Count)
                    {
                        // то, что уходит на сделку
                        var residual = new Order(buyOrd, new Gingerbread(sellOrd.Gingerbread.Count, buyOrd.Gingerbread.Price));
                        OrderService.AddOrder(residual);

                        // столько пряников не купили
                        buyOrd.Gingerbread.Count = buyOrd.Gingerbread.Count - sellOrd.Gingerbread.Count;
                        GingerbreadService.UpdateGingerbread(buyOrd.Gingerbread);
                        OrderService.UpdateOrder(buyOrd);

                        residual.OrderStatus = Status.OnConfirmation;
                        sellOrd.OrderStatus = Status.OnConfirmation;
                        var completedDeal = new History(residual, sellOrd, buyOrd.Gingerbread.Price);
                        HistoryService.AddHistory(completedDeal);

                        done = true;
                    }
                    else // if (sellOrd.Gingerbread.Count > bOrd.Gingerbread.Count)
                    {
                        // заказ, уходящий на сделку
                        var residual = new Order(sellOrd, new Gingerbread(buyOrd.Gingerbread.Count, buyOrd.Gingerbread.Price));
                        OrderService.AddOrder(residual);

                        // осталось пряников у продаца
                        sellOrd.Gingerbread.Count = sellOrd.Gingerbread.Count - buyOrd.Gingerbread.Count;
                        GingerbreadService.UpdateGingerbread(sellOrd.Gingerbread);

                        buyOrd.OrderStatus = Status.OnConfirmation;
                        residual.OrderStatus = Status.OnConfirmation;
                        var completedDeal = new History(buyOrd, residual, buyOrd.Gingerbread.Price);
                        HistoryService.AddHistory(completedDeal);
                    }
                    i++;
                } while (selected.Count > i && !done);
            }
        }

        IndexVM GetCompositeViewModel()
        {
            var currencyString = HttpContext.Response.Cookies["Currency"].Value;

            var targetCurrency = api.GetCurrencyModel(currencyString);

            var buyOrderVMList = api.GetBuyOrdersModel().Select(t => new OrderVM(t)).ToList();

            var sellOrderVMList = api.GetSellOrdersModel().Select(t => new OrderVM(t)).ToList();

            var historyOrderVMList = api.GetConfirmedHistoriesModel().Select(t => new HistoryVM(t)).ToList();

            return new IndexVM() { BuyVMList = buyOrderVMList, SellVMList = sellOrderVMList
                                    , HistoryVMList = historyOrderVMList, CurrentCurrency = targetCurrency};
        }

    }
}