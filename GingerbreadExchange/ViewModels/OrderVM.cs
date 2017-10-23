using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GingerbreadExchange.Models;

namespace GingerbreadExchange.ViewModels
{
    public class OrderVM
    {
        public double Price { get; set; }

        public int Count { get; set; }

        public string Email { get; set; }

        public OrderVM(Order ord)
        {
            Price = ord.Gingerbread.Price;
            Count = ord.Gingerbread.Count;
            Email = ord.Email;
        }
    }
}