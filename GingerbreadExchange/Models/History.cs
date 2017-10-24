﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GingerbreadExchange.Models
{
    public class History
    {
        public History()
        { }

        public History(Order buyOrder, Order sellOrder, double price)
        {
            var sellCount = sellOrder.Gingerbread.Count;
            var buyCount = buyOrder.Gingerbread.Count;

            Count = buyCount <= sellCount ? buyCount : sellCount;
            DealTime = DateTime.Now;
            BuyOrderTime = buyOrder.CreationTime;
            SellOrderTime = sellOrder.CreationTime;
            Price = price;
            BuyEmail = buyOrder.Email.Length == 0 ? "--" : buyOrder.Email;
            SellEmail = sellOrder.Email.Length == 0 ? "--" : sellOrder.Email;
        }

        [Key]
        public long Id { get; set; }

        public DateTime DealTime { get; set; }

        public DateTime BuyOrderTime { get; set; }

        public DateTime SellOrderTime { get; set; }

        public int Count { get; set; }

        public double Price { get; set; }

        public string BuyEmail { get; set; }

        public string SellEmail { get; set; }

        //[NotMapped]
        //public string NotSold { get { return Group.GroupName; } set { } }
    }
}