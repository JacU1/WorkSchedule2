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
                new Deal{Id = 1, Role = "Worker" ,DealType= "Contract of mandate", PayPerHour = 15, PayPerMonth = 0},
                new Deal{Id = 2, Role = "Instructor" ,DealType= "Contract of employment", PayPerHour = 0, PayPerMonth = 2500},
                new Deal{Id = 3, Role = "Menager", DealType = "Contract of employment" , PayPerHour = 0, PayPerMonth = 4000}
            };
        public Deal GetDealById(int Id)
        {
            return AllDeals.FirstOrDefault(d => d.Id == Id);
        }
    }
}
