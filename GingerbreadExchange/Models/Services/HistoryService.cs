using System;
using System.Collections;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GingerbreadExchange.Models.Services
{
    /// <summary>
    /// Сервис взаимодействия модели завершённых сделок с БД
    /// </summary>
    public class HistoryService
    {
        public static IList QueryHistories()
        {
            var result = SqlBuilder.Select<History>();
            result = result.Include(t => t.BuyOrder);
            result = result.Include(t => t.SellOrder);
            return result.ToList();
        }

        public static bool DeleteHistory(History h)
        {
            return SqlBuilder.Delete<History>(h);
        }

        public static bool AddHistory(History h)
        {
            return SqlBuilder.Add<History>(h);
        }

        public static bool UpdateHistory(History h)
        {
            return SqlBuilder.Update<History>(h);
        }
    }
}