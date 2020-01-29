using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkSchedule2.Models
{
    public class Shift
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public DateTime Date { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Supervisor { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
