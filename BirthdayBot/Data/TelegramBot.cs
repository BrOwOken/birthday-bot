using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BirthdayBot.Data
{
    class TelegramBot
    {
        private ITelegramBotClient _botClient { get; set; }
        public TelegramBot()
        {
            _botClient = new TelegramBotClient("");

            _botClient.OnMessage += Bot_OnMessage;

            _botClient.StartReceiving();
        }
        async void Bot_OnMessage(object sender, MessageEventArgs e)
        {

        }
    }
}
