﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace entityPractice.Models
{
    public class Customer
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string _token { get; set; }
    }
}