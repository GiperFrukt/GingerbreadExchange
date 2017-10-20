using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GingerbreadExchange.Models
{
    public class History
    {
        [Key]
        public int Id { get; set; }

        public DateTime DealTime { get; set; }

        public DateTime BuyOrderTime { get; set; }

        public DateTime SellOrderTime { get; set; }

        public int Count { get; set; }

        public double Price { get; set; }

        public string BuyEmail { get; set; }

        public string SellEmail { get; set; }
    }
}