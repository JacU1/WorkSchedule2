using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkSchedule2.Models
{
    public interface IUserRepository
    {
        IEnumerable<User> AllUsers { get;}
        IEnumerable<User> AvailableUsers { get; }
        IEnumerable<User> AdminUsers { get; }
        User GetUserById(int Id);

    }
}
