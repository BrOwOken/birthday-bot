using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BirthdayBot.Data.Models
{
    public class Birthday
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool IsYear { get; set; }
        public Birthday(int userId, string name, DateTime date, bool isYear)
        {
            UserId = userId;
            Name = name;
            Date = date;
            IsYear = isYear;
        }
    }
}
