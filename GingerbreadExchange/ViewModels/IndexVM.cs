using GingerbreadExchange.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GingerbreadExchange.ViewModels
{
    public class IndexVM
    {
        public IEnumerable<OrderVM> BuyVMList { get; set; }
        public IEnumerable<OrderVM> SellVMList { get; set; }
        public IEnumerable<HistoryVM> HistoryVMList { get; set; }

        [Required]
        public GingerbreadVM GingerbreadVM { get; set; }

        [Required]
        public OrderVM OrderVM { get; set; }

        public HistoryVM HistoryVM { get; }
    }
}