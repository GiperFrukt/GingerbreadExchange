using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GingerbreadExchange.Models;
using System.ComponentModel.DataAnnotations;

namespace GingerbreadExchange.ViewModels
{
    /// <summary>
    /// Отображение истории совершённых операций для именования соответствующей таблицы на странице
    /// </summary>
    public class HistoryVM
    {
        [Display(Name = "Выполнено")]
        public DateTime DealTime { get; set; }

        [Display(Name = "Дата К*")]
        public DateTime BuyOrderTime { get; set; }

        [Display(Name = "Дата П*")]
        public DateTime SellOrderTime { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Кол-во")]
        public int Count { get; set; }

        [Display(Name = "E-mail К")]
        public string BuyEmail { get; set; }

        [Display(Name = "E-mail П")]
        public string SellEmail { get; set; }

        public HistoryVM(History hist)
        {
            DealTime = hist.DealTime;
            BuyOrderTime = hist.BuyOrderTime;
            SellOrderTime = hist.SellOrderTime;
            Count = hist.Count;
            Price = hist.Price;
            BuyEmail = hist.BuyEmail;
            SellEmail = hist.SellEmail;
        }
    }
}