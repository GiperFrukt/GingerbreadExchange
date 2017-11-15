using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GingerbreadExchange.ViewModels;

namespace GingerbreadExchange.Models
{
    public enum CurrencyList
    {
        Rur,
        Usd
    }

    public class Currency
    {
        public Currency()
        { }

        public Currency(CurrencyList cur, decimal attitude)
        {
            Current = cur;
            AttitudeToRuble = attitude;
        }

        [Key]
        public long Id { get; set; }

        public CurrencyList Current { get; set; }

        public decimal AttitudeToRuble { get; set; }
    }
}