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
            //GingerbreadId = o.GingerbreadId;
            Gingerbread = o.Gingerbread;
            CreationTime = o.CreationTime;
            Email = o.Email;
        }

        public Order(Deal dealOperation, Gingerbread gingerbread, string email = "")
        {
            DealOperation = dealOperation;
            //GingerbreadId = gingerbread.Id;
            Gingerbread = gingerbread;
            CreationTime = DateTime.Now;
            Email = email;
            //gingerbread.Order = this;
        }

        [Key]
        public long Id { get; set; }

        public Deal DealOperation { get; set; }

        //public long GingerbreadId { get; set; }

        public Gingerbread Gingerbread { get; set; }

        public DateTime CreationTime { get; set; }

        [System.ComponentModel.DefaultValue(" ")]
        public string Email { get; set; }
    }
}