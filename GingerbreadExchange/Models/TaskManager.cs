using GingerbreadExchange.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var histories = HistoryService.QueryHistories() as List<History>;
            var historyOrder = histories.Where(t => t.Id == historyId).First();
            if (isConfirm)
            {
                historyOrder.Confirmed = true;
                var bOrd = historyOrder.BuyOrder;
                var sOrd = historyOrder.SellOrder;
                bOrd.OrderStatus = Status.Complited;
                sOrd.OrderStatus = Status.Complited;

                HistoryService.UpdateHistory(historyOrder);
                OrderService.UpdateOrder(bOrd);
                OrderService.UpdateOrder(sOrd);
            }
            else
            {
                var bOrd = historyOrder.BuyOrder;
                var sOrd = historyOrder.SellOrder;

                bOrd.OrderStatus = Status.Default;
                sOrd.OrderStatus = Status.Default;
                OrderService.UpdateOrder(bOrd);
                OrderService.DeleteOrder(sOrd);
                HistoryService.DeleteHistory(historyOrder);
            }
        }

    }
}