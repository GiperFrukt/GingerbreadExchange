using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GingerbreadExchange.ViewModels;

namespace GingerbreadExchange.Models
{
    /// <summary>
    /// Тип совершаемой операции
    /// </summary>
    public enum Deal
    {
        Buy,
        Sell
    }

    public enum Status
    {
        OnConfirmation,
        Complited,
        Default
    }

    /// <summary>
    /// Модель Заказа
    /// </summary>
    public class Order
    {
        public Order()
        { }

        public Order(Order o, Gingerbread g)
        {
            Id = o.Id;
            DealOperation = o.DealOperation;
            Gingerbread = g;
            CreationTime = o.CreationTime;
            Email = o.Email;
            OrderStatus = Status.Default;
        }

        /// <summary>
        /// Конструктор для инициализации БД
        /// </summary>
        /// <param name="dealOperation"></param>
        /// <param name="gingerbread"></param>
        /// <param name="email"></param>
        public Order(Deal dealOperation, Gingerbread gingerbread, string email = null)
        {
            DealOperation = dealOperation;
            Gingerbread = gingerbread;
            CreationTime = DateTime.Now;
            Email = email;
            OrderStatus = Status.Default;
        }

        [Key]
        public long Id { get; set; }

        public Deal DealOperation { get; set; }

        public Gingerbread Gingerbread { get; set; }

        public DateTime CreationTime { get; set; }

        public string Email { get; set; }

        public Status OrderStatus { get; set; }

        //[NotMapped]
        //public Gingerbread GetGingerbread { get { return Gingerbread; } set { } }
    }
}