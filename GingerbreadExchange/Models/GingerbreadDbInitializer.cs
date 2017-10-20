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
            db.Gingerbreads.Add(new Gingerbread { Count = 5, Price = 220 });
            //db.Gingerbreads.Add(new Gingerbread { Count = 6, Price = 180 });
            //db.Gingerbreads.Add(new Gingerbread { Count = 10, Price = 150 });

            db.Orders.Add(new Order { /*DealOperation = Deal.Buy, */CreationTime=DateTime.Now, Email="1111@11.com" });

            db.SaveChanges();

            base.Seed(db);
        }
    }
}