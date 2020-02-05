using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkSchedule2.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public string Position { get; set; }    //serwis, kuchnia, kasa itp 
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Hoursworked { get; set; }
        public string Supervisor { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
