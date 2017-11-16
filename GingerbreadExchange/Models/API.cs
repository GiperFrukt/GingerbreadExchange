using GingerbreadExchange.Models.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GingerbreadExchange.Models
{
    public class API
    {
        private ExchangeContext context = new ExchangeContext();

        public bool Add(object order)
        {
            try
            {
                context.Entry(order).State = EntityState.Added;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //public bool Add(Gingerbread gbread)
        //{
        //    try
        //    {
        //        context.Entry(gbread).State = EntityState.Added;
        //        context.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        //public bool AddOrder(Order order)
        //{
        //    try
        //    {
        //        context.Entry(order).State = EntityState.Added;
        //        context.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        #region Select

        public List<Order> GetBuyOrdersModel()
        {
            var table = context.Set<Order>();
            var buyOrders = table.Include(t => t.Gingerbread).Where(t => t.DealOperation == Deal.Buy && t.OrderStatus == Status.Default)
                .OrderByDescending(p => p.Gingerbread.Price);

            return buyOrders.ToList();
        }

        public List<Order> GetSellOrdersModel()
        {
            var table = context.Set<Order>();
            var sellOrders = table.Include(t => t.Gingerbread).Where(t => t.DealOperation == Deal.Sell && t.OrderStatus == Status.Default)
                .OrderBy(p => p.Gingerbread.Price);

            return sellOrders.ToList();
        }

        public List<History> GetConfirmedHistoriesModel()
        {
            var table = context.Set<History>();
            var histories = table.Where(t => t.Confirmed == true);

            return histories.ToList();
        }

        public List<History> GetUnconfirmedHistoriesModel()
        {
            var table = context.Set<History>();
            var histories = table.Where(t => t.Confirmed == false);

            return histories.ToList();
        }

        public Currency GetCurrencyModel(string targetCurrency)
        {
            var table = context.Set<Currency>();
            var currencies = table.Where(t => t.Current.ToString() == targetCurrency);

            return currencies.First();
        }

        public void GetCompositeModel()
        {

        } 

        #endregion
    }
}