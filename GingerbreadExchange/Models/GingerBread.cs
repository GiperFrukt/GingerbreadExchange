using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace GingerbreadExchange.Models
{
    /// <summary>
    /// Модель продукта
    /// </summary>
    public class Gingerbread
    {
        public Gingerbread()
        { }

        public Gingerbread(Gingerbread gb)
        {
            Id = gb.Id;
            Count = gb.Count;
            Price = gb.Price;
        }

        public Gingerbread(int count, decimal price)
        {
            Count = count;
            Price = price;
        }

        [Key]
        public long Id { get; set; }
        
        public int Count { get; set; }

        public decimal Price { get; set; }
    }
}