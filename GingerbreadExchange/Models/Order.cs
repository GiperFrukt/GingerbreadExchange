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
        [Key]
        public int Id { get; set; }

        public Deal DealOperation { get; set; }

        public int GingerbreadId { get; set; }

        [ForeignKey("GingerbreadId")]
        public Gingerbread Gingerbread { get; set; }

        public DateTime CreationTime { get; set; }

        public string Email { get; set; }
    }
}