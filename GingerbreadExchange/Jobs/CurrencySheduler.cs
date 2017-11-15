using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;

namespace GingerbreadExchange.Jobs
{
    public class CurrencySheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<CurrencyUpdater>().Build();

            ITrigger trigger = TriggerBuilder.Create()  // создаем триггер
                .WithIdentity("trigger1", "group1")     // идентифицируем триггер с именем и группой
                .StartNow()                            // запуск сразу после начала выполнения
                .WithSimpleSchedule(x => x            // настраиваем выполнение действия
                    .WithIntervalInHours(12)          // через 12 часов
                    .RepeatForever())                   // бесконечное повторение
                .Build();                               // создаем триггер

            scheduler.ScheduleJob(job, trigger);        // начинаем выполнение работы
        }
    }
}