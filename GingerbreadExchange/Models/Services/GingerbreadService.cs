﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GingerbreadExchange.Models.Services
{
    /// <summary>
    /// Сервис взаимодействия модели продукта с БД
    /// </summary>
    public class GingerbreadService
    {
        public static IList QueryGingerbreads()
        {
            var sqlBiulder = new SqlBuilder();
            var result = sqlBiulder.Select<Gingerbread>().ToList();
            return result;
        }

        public static bool DeleteGingerbread(Gingerbread gb)
        {
            var sqlBiulder = new SqlBuilder();
            return sqlBiulder.Delete<Gingerbread>(gb);
        }

        public static bool AddGingerbread(Gingerbread gb)
        {
            var sqlBiulder = new SqlBuilder();
            return sqlBiulder.Add<Gingerbread>(gb);
        }

        public static bool UpdateGingerbread(Gingerbread gb)
        {
            var sqlBiulder = new SqlBuilder();
            return sqlBiulder.Update<Gingerbread>(gb);
        }
    }
}