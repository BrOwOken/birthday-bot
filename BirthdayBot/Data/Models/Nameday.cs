﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BirthdayBot.Data.Models
{
    public class Nameday
    {
        [Key]
        public int Id { get; set; }
        public List<User> Watchers { get; set; }
        public string Name { get; set; }
        public byte Day { get; set; }
        public byte Month { get; set; }
        public Nameday()
        {
            Watchers = new List<User>();
        }
    }
}
