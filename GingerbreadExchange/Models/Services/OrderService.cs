using System;
using System.Collections;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GingerbreadExchange.Models.Services
{
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
            return sqlBiulder.Delete<Order>(o);
        }

        public static bool AddOrder(Order o)
        {
            var sqlBiulder = new SqlBuilder();
            return sqlBiulder.Add<Order>(o);
        }
    }
}