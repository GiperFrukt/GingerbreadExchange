using System;
using System.Collections;
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
            var sqlBiulder = new SqlBuilder();
            var result = sqlBiulder.Select<History>().ToList();
            return result;
        }

        public static bool DeleteHistory(History h)
        {
            var sqlBiulder = new SqlBuilder();
            return sqlBiulder.Delete<History>(h);
        }

        public static bool AddHistory(History h)
        {
            var sqlBiulder = new SqlBuilder();
            return sqlBiulder.Add<History>(h);
        }
    }
}