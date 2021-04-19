using BirthdayBot.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
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
            var thread = new Thread(new ThreadStart(Run));
            thread.Start();
        }
        private void Run()
        {
            int day = DateTime.Today.Day-1;
            while (true)
            {
                if(DateTime.Today.Day != day)
                {
                    if(DateTime.Today.Hour == 8 && DateTime.Today.Minute == 0)
                    {
                        day = DateTime.Today.Day;
                        NotifyUsers();
                    }
                }
                Thread.Sleep(55000);
            }
        }
        public void NotifyUsers()
        {
            foreach (var user in _dbContext.Users)
            {
                if(user.WatchedBirthdays.Count != 0)
                {
                    Dictionary<Birthday, int> birthdaysToNotify = new Dictionary<Birthday, int>();
                    foreach (var birthday in user.WatchedBirthdays)
                    {
                        var daysUntil = (birthday.Date.Date - DateTime.Today.Date).Days;
                        
                        if(daysUntil == 0)
                        {
                            birthdaysToNotify.Add(birthday, 0);
                        }
                        else if(daysUntil == 1)
                        {
                            birthdaysToNotify.Add(birthday, 1);
                        }
                        else if (daysUntil == 3)
                        {
                            birthdaysToNotify.Add(birthday, 3);
                        }
                        else if (daysUntil == 7)
                        {
                            birthdaysToNotify.Add(birthday, 7);
                        }
                    }
                    if(birthdaysToNotify.Count > 0)
                    {
                        string message = "⚠️ <b><u>BIRTHDAY</u></b> ALERT ⚠️\n\n";
                        var sortedBirthdays = SortBirthdays(birthdaysToNotify);
                        foreach (var bd in sortedBirthdays)
                        {

                        }
                    }
                }
            }
        }
        private Dictionary<Birthday, int> SortBirthdays(Dictionary<Birthday,int> birthdays)
        {
            var bd = birthdays.ToList();
            bd.Sort((b1, b2) => b1.Value.CompareTo(b2.Value));
            return bd.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
