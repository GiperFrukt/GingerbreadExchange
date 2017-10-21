using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace GingerbreadExchange.Models
{
    public class GingerbreadDbInitializer : DropCreateDatabaseIfModelChanges<ExchangeContext>
    {
        protected override void Seed(ExchangeContext db)
        {
            var g1 = new Gingerbread { Count = 5, Price = 220 };
            var g2 = new Gingerbread { Count = 6, Price = 180 };
            var g3 = new Gingerbread { Count = 10, Price = 150 };
            var g4 = new Gingerbread { Count = 7, Price = 500 };
            var g5 = new Gingerbread { Count = 100, Price = 600 };
            

            var o1 = new Order { CreationTime = DateTime.Now, DealOperation = Deal.Buy, Email = "first", Gingerbread = g1 };
            var o2 = new Order { CreationTime = DateTime.Now, DealOperation = Deal.Buy, Email = "second", Gingerbread = g2 };
            var o3 = new Order { CreationTime = DateTime.Now, DealOperation = Deal.Buy, Email = "third", Gingerbread = g3 };
            var o4 = new Order { CreationTime = DateTime.Now, DealOperation = Deal.Sell, Email = "123", Gingerbread = g4 };
            var o5 = new Order { CreationTime = DateTime.Now, DealOperation = Deal.Sell, Email = "1234", Gingerbread = g5 };

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


            //db.Gingerbreads.Add(new Gingerbread { Count = 5, Price = 220 });
            //db.Gingerbreads.Add(new Gingerbread { Count = 6, Price = 180 });
            //db.Gingerbreads.Add(new Gingerbread { Count = 10, Price = 150 });

            //db.SaveChanges();

            //Gingerbread a = db.Gingerbreads.First(t => t.Count == 10) as Gingerbread;

            //db.Orders.Add(new Order { /*DealOperation = Deal.Buy, */Gingerbread = a, CreationTime = DateTime.Now, Email = "1111@11.com" });

            db.SaveChanges();

            base.Seed(db);
        }
    }
}