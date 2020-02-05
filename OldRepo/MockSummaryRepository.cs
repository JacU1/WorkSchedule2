using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkSchedule2.Models
{
    public class MockSummaryRepository: ISummaryRepository
    {
        private readonly IUserRepository _userRepository = new MockUserRepository();

        public IEnumerable<Summary> AllSummary =>
            new List<Summary>
            {
                new Summary{Id = 1 , Bonus = 100, Grade = 4, TotalHoursWorked = 120, Salary = 2300, UserId = 1},
                new Summary{Id = 2 , Bonus = 200, Grade = 5, TotalHoursWorked = 120, Salary = 4000, UserId = 2},
            };
        public Summary GetSummaryById (int Id)
        {
            return AllSummary.FirstOrDefault(d => d.Id == Id);
        }
    }
}
