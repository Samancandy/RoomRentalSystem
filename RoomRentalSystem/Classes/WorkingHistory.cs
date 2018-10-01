using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomRentalSystem.Classes
{
    public class WorkingHistory
    {
        public string WHID { get; set; }
        public string WorkerID { get; set; }
        public string WorkingID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public WorkingHistory() { }
        public WorkingHistory(string whid, string workerid, string workingid, DateTime startdate, DateTime enddate)
        {
            this.WHID = whid; this.WorkerID = workerid; this.WorkingID = workingid;
            this.StartDate = startdate; this.EndDate = enddate;
        }
    }
}
