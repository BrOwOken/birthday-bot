using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace BirthdayBot.Data
{
    public class DayService
    {
        private TelegramBot _botClient;
        private ApplicationDbContext _dbContext;
        public DayService(TelegramBot botClient, ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _botClient = botClient;
        }
    }
}
