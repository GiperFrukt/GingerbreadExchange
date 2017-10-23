using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GingerbreadExchange.Models;

namespace GingerbreadExchange.ViewModels
{
    public class HistoryVM
    {
        public DateTime DealTime { get; set; }

        public DateTime BuyOrderTime { get; set; }

        public DateTime SellOrderTime { get; set; }

        public int Count { get; set; }

        public double Price { get; set; }

        public string BuyEmail { get; set; }

        public string SellEmail { get; set; }

        public HistoryVM(History hist)
        {
            DealTime = hist.DealTime;
            BuyOrderTime = hist.BuyOrderTime;
            SellOrderTime = hist.SellOrderTime;
            Count = hist.Count;
            Price = hist.Price;
            BuyEmail = hist.BuyEmail;
            SellEmail = hist.SellEmail;
        }
    }
}