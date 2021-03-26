using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BirthdayBot.Data.Models
{
    public class Nameday
    {
        [Key]
        public int Id { get; set; }
        public List<int> Watchers { get; set; }
        public string Name { get; set; }
        public byte Day { get; set; }
        public byte Month { get; set; }
    }
}
