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
            var user = Users.FirstOrDefault((u) => u.TelegramId == userId);
            user.WatchedBirthdays.Add(birthday);
            Entry(Users.FirstOrDefault((u) => u.TelegramId == userId)).CurrentValues.SetValues(user);
            SaveChanges();
        }
        public Birthday RemoveBirthday(int birthdayId)
        {
            var birthday = Birthdays.FirstOrDefault((b) => b.Id == birthdayId);
            var user = Users.FirstOrDefault((u) => u.Id == birthday.UserId);
            user.WatchedBirthdays.Remove(birthday);
            Entry(Users.FirstOrDefault((u) => u.Id == birthday.UserId)).CurrentValues.SetValues(user);
            Birthdays.Remove(birthday);
            SaveChanges();
            return birthday;
        }
        public List<Birthday> GetBirthdays(int userId)
        {
            var birthdays = Users.Include(u => u.WatchedBirthdays).First(u => u.TelegramId == userId).WatchedBirthdays;
            return birthdays;
        }
        //public int GetTelegramUserId(int userId)
        //{
        //    var user = Users.FirstOrDefault((u) => u.Id == userId));
        //    return user.TelegramId;
        //}
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
            Users.FirstOrDefault((u) => u.TelegramId == userId).WatchedNamedays.Add(Namedays.FirstOrDefault((n) => n.Id == namedayId));
            SaveChanges();
        }
    }
}
