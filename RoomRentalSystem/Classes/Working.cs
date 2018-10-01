using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomRentalSystem.Classes
{
    public class Working
    {
        public string WorkingID { get; set; }
        public string WorkerID { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
        public Working() { }
        public Working(string workingid, string workerid, string position, int salary)
        {
            this.WorkingID = workingid; this.WorkerID = workerid;
            this.Position = position; this.Salary = salary;
        }
        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}", WorkingID, WorkerID, Position, Salary);
        }
    }
}
