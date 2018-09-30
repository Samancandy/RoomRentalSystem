using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomRentalSystem.Classes
{
    internal class Worker : Person
    {
        private int Hour{get; set;}
        private float Rate{get; set;}
        public Worker() { }
        public Worker(string id, string name, string gender, DateTime dob, string phone, string address, int hour, float rate)
            : base(id, name, gender, dob, phone, address)
        {
            this.Hour = hour; this.Rate = rate;
        }
        private double GetSalary()
        {
            return Hour * Rate;
        }
    }
}
