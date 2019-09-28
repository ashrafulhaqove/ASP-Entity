using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace entityPractice.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int Customer_Id { get; set; }
        public string City { get; set; }
        public int Zip { get; set; }
        public string Details { get; set; }
    }
}