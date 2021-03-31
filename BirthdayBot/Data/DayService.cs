using BirthdayBot.Data.Models;
using Hangfire;
using Hangfire.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Telegram.Bot;

namespace BirthdayBot.Data
{
    public class DayService
    {
        private TelegramBot _botClient;
        private ApplicationDbContext _dbContext;
        private TimeSpan _notifySchedule;
        public DayService(TelegramBot botClient, ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _botClient = botClient;
            OnInitialized();
        }
        private void OnInitialized()
        {
            var options = new BackgroundJobServerOptions
            {
                SchedulePollingInterval = TimeSpan.FromMinutes(1)
            };
            var server = new BackgroundJobServer(options);
            //Expression<Action> notify = new Expression<Action>();
            //string v = BackgroundJob.Schedule(notify, _notifySchedule);
        }
        public void NotifyUsers()
        {
            ResetSchedule();
        }
        private void ResetSchedule()
        {
            //BackgroundJob.Schedule(NotifyUsers, new DateTime(2021, 3, 31));
        }
    }
}
