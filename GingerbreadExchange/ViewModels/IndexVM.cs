using GingerbreadExchange.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GingerbreadExchange.ViewModels
{
    /// <summary>
    /// Сборное отображение для визулизации данных на странице
    /// </summary>
    public class IndexVM
    {
        /// <summary>
        /// Список отображений заказов на покупку
        /// </summary>
        public IEnumerable<OrderVM> BuyVMList { get; set; }

        /// <summary>
        /// Список отображений заказаов на продажу
        /// </summary>
        public IEnumerable<OrderVM> SellVMList { get; set; }

        /// <summary>
        /// Список отображений истории операций
        /// </summary>
        public IEnumerable<HistoryVM> HistoryVMList { get; set; }

        /// <summary>
        /// Отображения продукта для добваления в БД
        /// </summary>
        [Required]
        public GingerbreadVM GingerbreadVM { get; set; }

        /// <summary>
        /// Отображение заказа для добавления в БД
        /// </summary>
        [Required]
        public OrderVM OrderVM { get; set; }

        /// <summary>
        /// Отображение истории совершённых операций для именования соответствующей таблицы на странице
        /// </summary>
        public HistoryVM HistoryVM { get; }

        public Currency CurrentCurrency { get; set; }
    }
}