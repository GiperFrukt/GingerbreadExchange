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
        public static IEnumerable QueryOrders()
        {
            var sqlBiulder = new SqlBuilder();
            var result = sqlBiulder.Select<Order>();
            return result.Include(t => t.Gingerbread).ToList();
        }

        public static bool DeleteOrder(Order o)
        {
            var sqlBiulder = new SqlBuilder();
            GingerbreadService.DeleteGingerbread(o.Gingerbread);
            return sqlBiulder.Delete<Order>(o);
        }

        public static bool AddOrder(Order o)
        {
            var sqlBiulder = new SqlBuilder();
            return sqlBiulder.Add<Order>(o);
        }

        public static bool UpdateOrder(Order o)
        {
            var sqlBiulder = new SqlBuilder();
            return sqlBiulder.Update<Order>(o);
        }
    }
}