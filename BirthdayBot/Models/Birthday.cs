using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BirthdayBot.Models
{
    class Birthday
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Day { get; set; }
        public byte Month { get; set; }
        public int Year { get; set; }
    }
}
