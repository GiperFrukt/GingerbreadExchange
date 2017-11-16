using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GingerbreadExchange.ViewModels;
using GingerbreadExchange.Models;
using GingerbreadExchange.Models.Services;

namespace GingerbreadExchange.Jobs
{
    public class CurrencyUpdater : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var api = new API();

            var date = DateTime.Today.ToShortDateString().Replace('.', '/');
            var document = XDocument.Load("http://www.cbr.ru/scripts/XML_daily.asp?date_req="+date);
            var element = document.Root.Elements().First(x => x.Attribute("ID").Value == "R01235");
            var attitude = (element.LastNode as XElement).Value;

            var targetCurrency = api.GetCurrencyModel("Usd");
            targetCurrency.AttitudeToRuble = Decimal.Parse(attitude);

            api.Update(targetCurrency);
        }
    }
}