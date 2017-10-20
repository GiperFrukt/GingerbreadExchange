using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GingerbreadExchange.Models
{
    public enum Deal
    {
        Buy,
        Sell
    }

    public class Order
    {
        
        public int Id { get; set; }

        public Deal DealOperation { get; set; }

        public DateTime CreationTime { get; set; }

        public string Email { get; set; }
    }
}