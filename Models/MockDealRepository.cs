using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkSchedule2.Models
{
    public class MockDealRepository : IDealRepository
    {
        public IEnumerable<Deal> AllDeals =>
            new List<Deal>
            {
                new Deal{Id = 1, Role = "Serwisant" ,DealType= "Umowa zlecenia", PayPerHour = 15, PayPerMonth = 0},
                new Deal{Id = 2, Role = "Kucharz" ,DealType= "Umowa o prace", PayPerHour = 0, PayPerMonth = 2500}
            };
        public Deal GetDealById(int Id)
        {
            return AllDeals.FirstOrDefault(d => d.Id == Id);
        }
    }
}
