using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace GingerbreadExchange.Models
{
    public class GingerbreadDbInitializer : DropCreateDatabaseAlways<ExchangeContext>
    {
        protected override void Seed(ExchangeContext db)
        {
            var g1 = new Gingerbread(5, 220);
            var g2 = new Gingerbread(6, 180); 
            var g3 = new Gingerbread(10, 150);
            var g4 = new Gingerbread(7, 500); 
            var g5 = new Gingerbread(100, 600);
            

            var o1 = new Order(Deal.Buy, g1, "first@gmail.com");
            var o2 = new Order(Deal.Buy, g2, "second@mail.ru");
            var o3 = new Order(Deal.Buy, g3);
            var o4 = new Order(Deal.Sell, g4, "123");
            var o5 = new Order(Deal.Sell, g5, "mail@mail.ru");

            db.Gingerbreads.Add(g1);
            db.Gingerbreads.Add(g2);
            db.Gingerbreads.Add(g3);
            db.Gingerbreads.Add(g4);
            db.Gingerbreads.Add(g5);

            db.Orders.Add(o1);
            db.Orders.Add(o2);
            db.Orders.Add(o3);
            db.Orders.Add(o4);
            db.Orders.Add(o5);

            db.SaveChanges();

            base.Seed(db);
        }
    }
}