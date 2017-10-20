using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace GingerbreadExchange.Models
{
    public class ExchangeContext : DbContext
    {
        public ExchangeContext() : base("ExchangeDb")
        { }

        public DbSet<Gingerbread> Gingerbreads { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<History> Histories { get; set; }
    }
}