using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkSchedule2.Models
{
    public class MockShiftRepository: IShiftRepository
    {
        private readonly IUserRepository _userRepository = new MockUserRepository();

        public IEnumerable<Shift> AllShifts =>
            new List<Shift>
            {
                new Shift{ Id = 1, Position="Kitchen",
                    Start=DateTime.Parse("06.02.2020 8:00"), End=DateTime.Parse("06.02.2020 14:00"), Hoursworked = 6,
                    Supervisor = _userRepository.AllUsers.ToList()[0].ToString(), UserId = 1 }
            };
        public Shift GetShiftById(int Id)
        {
            return AllShifts.FirstOrDefault(s => s.Id == Id);
        }

    }
}
