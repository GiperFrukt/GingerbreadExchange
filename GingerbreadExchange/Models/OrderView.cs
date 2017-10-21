using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GingerbreadExchange.Models
{
    public class OrderView
    {
        public double Price { get; set; }

        public int Count { get; set; }

        public string Email { get; set; }
    }
}