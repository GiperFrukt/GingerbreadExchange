﻿using System;
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
        ExchangeContext db = new ExchangeContext();

        public bool Update<T>(T valueToUpdate) where T : class
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

        public bool Add<T>(T valueToAdd) where T : class
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

        public bool Delete<T>(T valueToDel) where T : class
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


        public IQueryable<T> Select<T>() where T : class
        {
            var table = FindTable<T>();
            return table;
        }

        /// <summary>
        /// Нахождение определённой таблицы в контексте в зависимости от модели таблицы
        /// </summary>
        /// <typeparam name="T">Класс модели таблицы</typeparam>
        private DbSet<T> FindTable<T>() where T : class
        {
            DbSet<T> targetTable = null;

            var type = typeof(ExchangeContext);
            var properties = type.GetProperties(
            BindingFlags.Instance
            | BindingFlags.Public);
            string tableName = typeof(T).Name;
            var tmp = properties.First(t => t.Name == (string.Format("{0}s", tableName)) || t.Name == (string.Format("{0}ies", tableName.Remove(tableName.Count() - 1))));
            targetTable = tmp.GetValue(db) as DbSet<T>;

            return targetTable;
        }
    }
}
