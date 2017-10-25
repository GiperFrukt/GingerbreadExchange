using GingerbreadExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GingerbreadExchange.ViewModels
{
    public class IndexVM
    {
        public IEnumerable<OrderVM> BuyVMList { get; set; }
        public IEnumerable<OrderVM> SellVMList { get; set; }
        public IEnumerable<HistoryVM> HistoryVMList { get; set; }
        public GingerbreadVM GingerbreadVM { get; set; }
        public OrderVM OrderVM { get; set; }
    }
}