using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using GingerbreadExchange.Models;

namespace GingerbreadExchange.ViewModels
{
    /// <summary>
    /// Отображение продукта
    /// </summary>
    public class GingerbreadVM
    {
        [Required]
        [Display(Name = "Количество")]
        [Range(1, int.MaxValue, ErrorMessage = "Недопустимое количество")]
        public int Count { get; set; }

        [Required]
        [Display(Name = "Цена")]
        [Range(typeof(decimal), "0", "9999999999999", ErrorMessage = "Недопустимая цена")]
        public decimal Price { get; set; }

        public GingerbreadVM(int count, decimal price)
        {
            Count = count;
            Price = price;
        }

        public GingerbreadVM(Gingerbread gb)
        {
            Count = gb.Count;
            Price = gb.Price;
        }

        public GingerbreadVM()
        { }
    }
}