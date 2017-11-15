using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace GingerbreadExchange.Models
{
    /// <summary>
    /// Класс взаимодействия с БД
    /// </summary>
    public class SqlBuilder
    {
        static public ExchangeContext db = new ExchangeContext();

        public static bool Update<T>(T valueToUpdate) where T : class
        {
            try
            {
                db.Entry(valueToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool Add<T>(T valueToAdd) where T : class
        {
            try
            {
                db.Entry(valueToAdd).State = EntityState.Added;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool Delete<T>(T valueToDel) where T : class
        {
            try
            {
                db.Entry(valueToDel).State = EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static IQueryable<T> Select<T>() where T : class
        {
            var table = FindTable<T>();
            return table;
        }

        /// <summary>
        /// Нахождение определённой таблицы в контексте в зависимости от модели таблицы
        /// </summary>
        /// <typeparam name="T">Класс модели таблицы</typeparam>
        private static DbSet<T> FindTable<T>() where T : class
        {
            return db.Set<T>();
        }
    }
}
