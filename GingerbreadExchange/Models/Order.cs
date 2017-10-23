using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GingerbreadExchange.Models
{
    public enum Deal
    {
        Buy,
        Sell
    }

    public class Order
    {
        public Order()
        { }

        public Order(Order o)
        {
            Id = o.Id;
            DealOperation = o.DealOperation;
            Gingerbread = o.Gingerbread;
            CreationTime = o.CreationTime;
            Email = o.Email;
        }

        public Order(Deal dealOperation, Gingerbread gingerbread, string email = "")
        {
            DealOperation = dealOperation;
            Gingerbread = gingerbread;
            CreationTime = DateTime.Now;
            Email = email;
        }

        [Key]
        public long Id { get; set; }

        public Deal DealOperation { get; set; }

        public Gingerbread Gingerbread { get; set; }

        public DateTime CreationTime { get; set; }

        public string Email { get; set; }
    }
}