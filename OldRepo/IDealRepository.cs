using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkSchedule2.Models
{
    public interface IDealRepository
    {
        IEnumerable<Deal> AllDeals { get;}
        Deal GetDealById(int Id);
    }
}
