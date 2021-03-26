using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BirthdayBot.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public List<int> WatchedNamedays { get; set; }
        public List<int> WatchedBirthdays { get; set; }
    }
}
