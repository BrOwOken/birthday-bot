using BirthdayBot.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirthdayBot.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Nameday> Namedays { get; set; }
        public DbSet<Birthday> Birthdays { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public void AddBirthday(Birthday birthday, int userId)
        {
            Birthdays.Add(birthday);
            Users.FirstOrDefault((u) => u.TelegramId == userId).WatchedBirthdays.Add(birthday.Id);
            SaveChanges();
        }
        public void RemoveBirthday(int birthdayId)
        {
            var birthday = Birthdays.FirstOrDefault((b) => b.Id == birthdayId);
            Birthdays.Remove(birthday);
            Users.FirstOrDefault((u) => u.TelegramId == birthday.UserId).WatchedBirthdays.Remove(birthdayId);
        }
        public List<Birthday> GetBirthdays(int userId)
        {
            var birhtdaysIds = Users.FirstOrDefault((u) => u.TelegramId == userId).WatchedBirthdays;
            List<Birthday> birthdays = new List<Birthday>();
            foreach (var id in birhtdaysIds)
            {
                birthdays.Add(Birthdays.FirstOrDefault((b) => b.Id == id));
            }
            return birthdays;
        }
        public void UserInit(int id)
        {
            if (Users.FirstOrDefault((u) => u.TelegramId == id) != null) return;
            else Users.Add(new User(id));
            SaveChanges();
        }
        // public void AddBirthdayToUser(int birthdayId, int userId)
        // {
        //     Users.FirstOrDefault((u) => u.TelegramId == userId).WatchedBirthdays.Add(birthdayId);
        //     SaveChanges();
        // }
        public void AddNamedayToUser(int namedayId, int userId)
        {
            Users.FirstOrDefault((u) => u.TelegramId == userId).WatchedNamedays.Add(namedayId);
            SaveChanges();
        }
    }
}
