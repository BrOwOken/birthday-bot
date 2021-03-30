using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BirthdayBot.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public int TelegramId { get; set; }
        [NotMapped]
        public List<int> WatchedNamedays { get; set; }
        [NotMapped]
        public List<int> WatchedBirthdays { get; set; }
        public User(int id)
        {
            TelegramId = id;
            WatchedNamedays = new List<int>();
            WatchedBirthdays = new List<int>();
        }
    }
}
