using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkSchedule2.Models
{
    public class Vacation
    {
        public int Id { get; set; }
        public string ComplainText { get; set; }    
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string type { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
