using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkSchedule2.Models
{
    public class ViewModel
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string user_name { get; set; }
        public string user_last_name { get; set; }
        public string supervisor_name { get; set; }
        public string supervisor_last_name { get; set; }
    }
}
