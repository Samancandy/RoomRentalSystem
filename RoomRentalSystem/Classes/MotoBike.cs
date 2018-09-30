using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomRentalSystem.Classes
{
    class MotoBike : Vehicle
    {
        //private string Power { get; set; }
        //private string Plate { get; set; }
        public MotoBike() { }
        public MotoBike(string id, string type, string model, string color, string power, string plate)
        :base(id, type, model, color, power, plate){
            
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
