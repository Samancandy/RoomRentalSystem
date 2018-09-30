using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomRentalSystem
{
    public class Vehicle
    {
        public string ID { get; set; }
        public string VehicleType { set; get; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Power { get; set; }
        public string Plate { get; set; }
        public Vehicle()
        {
            //ID = "0001"; Model = "Dream"; Color = "black";
        }
        public Vehicle(string id, string type, string model, string color, string power, string plate)
        {
            this.ID = id; this.VehicleType = type; this.Model = model;
            this.Color = color; this.Power = power; this.Plate = plate;
        }
    }
}
