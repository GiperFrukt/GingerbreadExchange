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
        public long Id { get; set; }

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

        [Display(Name = "Подтверждено")]
        public bool Confirmed { get; set; }

        public HistoryVM(History hist)
        {
            Id = hist.Id;
            DealTime = hist.DealTime;
            BuyOrderTime = hist.BuyOrder.CreationTime;
            SellOrderTime = hist.SellOrder.CreationTime;
            Count = hist.Count;
            Price = hist.Price;
            BuyEmail = hist.BuyOrder.Email;
            SellEmail = hist.SellOrder.Email;
            Confirmed = hist.Confirmed;
        }
    }
}