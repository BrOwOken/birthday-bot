using BirthdayBot.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace BirthdayBot.Data
{
    public class TelegramBot
    {
        private ITelegramBotClient _botClient { get; set; }
        private ApplicationDbContext _dbContext;
        private Dictionary<int, UserStatus> clients;
        public TelegramBot(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            _botClient = new TelegramBotClient("1719454786:AAHQta2PjYRu3G7PD-2f7Op-2XhlUE3EhDA");
            clients = new Dictionary<int, UserStatus>();

            _botClient.OnMessage += OnMessage;
            _botClient.OnCallbackQuery += OnCallbackQuery;

            _botClient.StartReceiving();
        }
        private void CheckClient(int id)
        {
            _dbContext.UserInit(id);
            if (!clients.ContainsKey(id))
            {
                clients.Add(id, new UserStatus());
            }
            else return;
        }
        private bool SaveDate(string text, int id)
        {
            if (DateTime.TryParseExact(text, "dd.M.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
            {
                clients[id].BirthdayDate = date;
                clients[id].BirthdayYear = true;
                return true;
            }
            else if (DateTime.TryParseExact(text, "dd.M", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateM))
            {
                clients[id].BirthdayDate = dateM;
                clients[id].BirthdayYear = false;
                return true;
            }
            else return false;
        }
        async void OnMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            CheckClient(message.From.Id);
            if (message.Text.ToLower().StartsWith("/add"))
            {
                if (message.Text.ToLower() == "/addbirthday")
                {
                    clients[message.From.Id].IsBirthdayCmd = true;
                    await _botClient.SendTextMessageAsync(message.Chat, "Give me the person's name!");
                }
                else if (message.Text.ToLower() == "/addnameday")
                {
                    clients[message.From.Id].IsBirthdayCmd = false;
                }
            }
            else if (message.Text.ToLower().StartsWith("/remove"))
            {
                clients[message.From.Id].IsBirthdayCmd = false;
                if (message.Text.ToLower() == "/removebirthday")
                {
                    
                    var buttons = new List<InlineKeyboardButton[]>();
                    var birthdays = _dbContext.GetBirthdays(message.From.Id);
                    if(birthdays.Count == 0) 
                    {
                        await _botClient.SendTextMessageAsync(message.Chat, "There are no birthdays to remove.");
                    }
                    else
                    {
                        for (int i = 0; i < birthdays.Count; i++)
                        {
                            if (birthdays[i].IsYear)
                            {
                                buttons.Add(new InlineKeyboardButton[]
                                {
                                    new InlineKeyboardButton()
                                    {
                                        Text = $"{birthdays[i].Name} - {birthdays[i].Date.Day}.{birthdays[i].Date.Month}.{birthdays[i].Date.Year}",
                                        CallbackData = $"rb_{birthdays[i].Id}"
                                    }
                                });
                            }
                            else
                            {
                                buttons.Add(new InlineKeyboardButton[]
                                {
                                    new InlineKeyboardButton()
                                    {
                                        Text = $"{birthdays[i].Name} - {birthdays[i].Date.Day}.{birthdays[i].Date.Month}.",
                                        CallbackData = $"rb_{birthdays[i].Id}"
                                    }
                                });
                            }
                        }
                        await _botClient.SendTextMessageAsync(message.Chat, "Click on birthday to remove.", replyMarkup: new InlineKeyboardMarkup(buttons));
                    }
                }
                else if (message.Text.ToLower() == "/removenameday")
                {

                }
            }
            else if (message.Text.ToLower().StartsWith("/show"))
            {
                clients[message.From.Id].IsBirthdayCmd = false;
                if (message.Text.ToLower() == "/showbirthdays")
                {
                    string response = string.Empty;
                    var bdays = _dbContext.GetBirthdays(message.From.Id);
                    if(bdays.Count == 0)
                    {
                        response = "No birthdays to show.";
                    }
                    else
                    {
                        response += "<b><u>Watched birthdays</u></b>\n\n";
                        for (int i = 0; i < bdays.Count; i++)
                        {
                            if (i == bdays.Count - 1)
                            {
                                if (bdays[i].IsYear)
                                {
                                    response += $"{bdays[i].Name} - {bdays[i].Date.Day}.{bdays[i].Date.Month}.{bdays[i].Date.Year}";
                                }
                                else
                                {
                                    response += $"{bdays[i].Name} - {bdays[i].Date.Day}.{bdays[i].Date.Month}";
                                }
                            }
                            else
                            {
                                if (bdays[i].IsYear)
                                {
                                    response += $"{bdays[i].Name} - {bdays[i].Date.Day}.{bdays[i].Date.Month}.{bdays[i].Date.Year}\n";
                                }
                                else
                                {
                                    response += $"{bdays[i].Name} - {bdays[i].Date.Day}.{bdays[i].Date.Month}\n";
                                }
                            }
                        }
                        
                    }
                    await _botClient.SendTextMessageAsync(message.Chat, response, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
                }
                else if (message.Text.ToLower() == "/removenameday")
                {

                }
            }
            else
            {
                if (message.Text != null)
                {
                    if (clients[message.From.Id].IsBirthdayCmd)
                    {
                        if (clients[message.From.Id].BirthdayName == null || clients[message.From.Id].BirthdayName == "")
                        {
                            clients[message.From.Id].BirthdayName = message.Text;
                            await _botClient.SendTextMessageAsync(message.Chat, "Please enter a date of their birhtday in this format: DD.MM.YYYY (if you don't know the year, then just enter 0 instead of the actual year.)");
                        }
                        else if (clients[message.From.Id].BirthdayName != null && clients[message.From.Id].BirthdayName != "")
                        {
                            if (SaveDate(message.Text, message.From.Id)) 
                            {
                                var client = clients[message.From.Id];
                                _dbContext.AddBirthday(new Birthday(message.From.Id, client.BirthdayName, client.BirthdayDate, client.BirthdayYear), message.From.Id);
                                var date = client.BirthdayDate;
                                clients[message.From.Id] = new UserStatus();
                                await _botClient.SendTextMessageAsync(message.Chat, $"Birthday added to your watchlist! ({date})");
                            }
                            else
                            {
                                await _botClient.SendTextMessageAsync(message.Chat, "Please enter a valid date!");
                            }
                        }
                    }
                }
                
            }
        }
        async void OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            var query = e.CallbackQuery.Data.Split('_');
            var action = query[0];
            var id = query[1];
            if (action == "rb")
            {
                var bday = _dbContext.RemoveBirthday(int.Parse(id));
                await _botClient.SendTextMessageAsync(e.CallbackQuery.Message.Chat, $"{bday.Name}'s birthday was removed from the watchlist.");
            }
            else if (action == "rn")
            {

            }
        }
    }
}
