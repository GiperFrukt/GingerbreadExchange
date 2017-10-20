using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GingerbreadExchange.Models
{
    public class Gingerbread
    {
        [Key]
        public int Id { get; set; }

        public int Count { get; set; }

        public double Price { get; set; }
    }
}