using GingerbreadExchange.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace GingerbreadExchange.Models
{
    public class TaskManager
    {
        private long historyId;
        private bool isConfirm;

        public TaskManager(long histId, bool isConfirm)
        {
            this.historyId = histId;
            this.isConfirm = isConfirm;
        }

        public void ConfirmDeal()
        {
            using (var transaction = SqlBuilder.db.Database.BeginTransaction())
            {
                var api = new API();
                Thread.Sleep(5000);
                try
                {
                    var historyOrder = api.GetHistoryById(historyId);
                    //var histories = HistoryService.QueryHistories() as List<History>;
                    //var historyOrder = histories.Where(t => t.Id == historyId).First();
                    if (isConfirm)
                    {
                        historyOrder.Confirmed = true;
                        var bOrd = historyOrder.BuyOrder;
                        var sOrd = historyOrder.SellOrder;
                        bOrd.OrderStatus = Status.Complited;
                        sOrd.OrderStatus = Status.Complited;

                        api.Update(historyOrder);
                        api.Update(bOrd);
                        api.Update(sOrd);
                        //HistoryService.UpdateHistory(historyOrder);
                        //OrderService.UpdateOrder(bOrd);
                        //OrderService.UpdateOrder(sOrd);
                    }
                    else
                    {
                        var bOrd = historyOrder.BuyOrder;
                        var sOrd = historyOrder.SellOrder;

                        bOrd.OrderStatus = Status.Default;
                        sOrd.OrderStatus = Status.Default;

                        api.Update(bOrd);
                        api.Delete(sOrd.Gingerbread);
                        api.Delete(sOrd);

                        api.Delete(historyOrder);

                        //OrderService.UpdateOrder(bOrd);
                        //OrderService.DeleteOrder(sOrd);
                        //HistoryService.DeleteHistory(historyOrder);
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            
        }

    }
}