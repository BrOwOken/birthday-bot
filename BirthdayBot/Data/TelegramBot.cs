using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BirthdayBot.Data
{
    public class TelegramBot
    {
        private ITelegramBotClient _botClient { get; set; }
        private ApplicationDbContext _dbContext;
        public TelegramBot(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            _botClient = new TelegramBotClient("1719454786:AAHDRuxRzcigoeb0vQnnc-uTyYJaSgLT3uA");

            _botClient.OnMessage += Bot_OnMessage;

            _botClient.StartReceiving();
        }
        async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message.Text.StartsWith("/add"))
            {
                if(message.Text.ToLower() == "/addbirthday")
                {

                }
                else if(message.Text.ToLower() == "/addnameday")
                {

                }
            }
            else if (message.Text.StartsWith("/remove"))
            {
                if (message.Text.ToLower() == "/removebirthday")
                {

                }
                else if (message.Text.ToLower() == "/removenameday")
                {

                }
            }
            else
            {

            }
        }
    }
}
