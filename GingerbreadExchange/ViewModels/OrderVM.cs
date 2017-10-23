using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GingerbreadExchange.ViewModels
{
    public class OrderVM
    {
        public double Price { get; set; }

        public int Count { get; set; }

        public string Email { get; set; }
    }
}