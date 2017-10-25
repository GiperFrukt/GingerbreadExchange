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
    public class GingerbreadVM
    {
        public long Id { get; set; }

        ////[Required(ErrorMessage = "Поле должно быть установлено")]
        [Required]
        public int Count { get; set; }

        [Required]
        public float Price { get; set; }

        public GingerbreadVM(int count, float price)
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