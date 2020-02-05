using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkSchedule2.Models
{
    public class Summary
    {
        public int Id { get; set; }
        public decimal TotalHoursWorked { get; set; }
        public int Grade { get; set; }
        public decimal Bonus { get; set; }
        public decimal Salary { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Shift Shift { get; set; }
        public Deal Deal { get; set; }
    }
}
