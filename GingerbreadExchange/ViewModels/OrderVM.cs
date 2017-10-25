using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GingerbreadExchange.Models;

namespace GingerbreadExchange.ViewModels
{
    public class OrderVM
    {
        public Deal DealOperation { get; set; }

        public GingerbreadVM GingerbreadVM { get; set; }

        public string Email { get; set; }

        public OrderVM(Order ord)
        {
            Email = ord.Email;
            DealOperation = ord.DealOperation;
            GingerbreadVM = new GingerbreadVM(ord.Gingerbread);
        }

        public OrderVM()
        { }
    }
}