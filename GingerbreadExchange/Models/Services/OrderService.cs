using System;
using System.Collections;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GingerbreadExchange.Models.Services
{
    /// <summary>
    /// Сервис взаимодействия модели Заказа с БД
    /// </summary>
    public class OrderService
    {
        public static IList QueryOrders()
        {
            var result = SqlBuilder.Select<Order>();
            return result.Include(t => t.Gingerbread).ToList();
        }

        public static bool DeleteOrder(Order o)
        {
            GingerbreadService.DeleteGingerbread(o.Gingerbread);
            return SqlBuilder.Delete<Order>(o);
        }

        public static bool AddOrder(Order o)
        {
            return SqlBuilder.Add<Order>(o);
        }

        public static bool UpdateOrder(Order o)
        {
            return SqlBuilder.Update<Order>(o);
        }
    }
}