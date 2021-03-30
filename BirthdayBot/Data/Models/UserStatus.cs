using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirthdayBot.Data.Models
{
    public class UserStatus
    {
        public bool IsBirthdayCmd { get; set; } = false;
        public string BirthdayName { get; set; }
        public bool BirthdayYear { get; set; }
        public DateTime BirthdayDate { get; set; }
    }
}
