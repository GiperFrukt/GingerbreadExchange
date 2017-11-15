using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace GingerbreadExchange.Models
{
    public class ExchangeContext : DbContext
    {
        public ExchangeContext()
        { }

        public DbSet<Gingerbread> Gingerbreads { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<History> Histories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GingerbreadConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new HistoryConfiguration());
        }

        public class GingerbreadConfiguration : EntityTypeConfiguration<Gingerbread>
        {
            public GingerbreadConfiguration()
            {
                this.Property(t => t.Count).IsRequired();
                this.Property(t => t.Price).IsRequired();
            }
        }

        public class OrderConfiguration : EntityTypeConfiguration<Order>
        {
            public OrderConfiguration()
            {
                this.Property(t => t.CreationTime).IsRequired();
                this.Property(t => t.DealOperation).IsRequired();
                this.Property(t => t.Email).HasMaxLength(50);
            }
        }

        public class HistoryConfiguration : EntityTypeConfiguration<History>
        {
            public HistoryConfiguration()
            {
                this.Property(t => t.Count).IsRequired();
                this.Property(t => t.DealTime).IsRequired();
                this.Property(t => t.Price).IsRequired();
            }
        }
    }
}