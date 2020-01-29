using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkSchedule2.Models
{
    public interface IShiftRepository
    {
        IEnumerable<Shift> AllShifts { get; set; }
        Shift GetShiftById(int Id);
    }
}
