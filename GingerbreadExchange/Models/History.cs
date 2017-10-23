using System;
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

        public History(Order buyOder, Order sellOrder)
        {
            var sellCount = sellOrder.Gingerbread.Count;
            var buyCount = buyOder.Gingerbread.Count;

            Count = buyCount <= sellCount ? buyCount : sellCount;
            DealTime = DateTime.Now;
            BuyOrderTime = buyOder.CreationTime;
            SellOrderTime = sellOrder.CreationTime;
            Price = sellOrder.Gingerbread.Price;
            BuyEmail = buyOder.Email;
            SellEmail = sellOrder.Email;
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