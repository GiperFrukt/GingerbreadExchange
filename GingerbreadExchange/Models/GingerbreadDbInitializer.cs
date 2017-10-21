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
            db.Gingerbreads.Add(new Gingerbread { Count = 5, Price = 220 });
            db.Gingerbreads.Add(new Gingerbread { Count = 6, Price = 180 });
            db.Gingerbreads.Add(new Gingerbread { Count = 10, Price = 150 });

            db.SaveChanges();

            Gingerbread a = db.Gingerbreads.First(t => t.Count ==10) as Gingerbread;

            db.Orders.Add(new Order { /*DealOperation = Deal.Buy, */Gingerbread=a, CreationTime =DateTime.Now, Email="1111@11.com" });

            db.SaveChanges();

            base.Seed(db);
        }
    }
}