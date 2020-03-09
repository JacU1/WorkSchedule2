using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkSchedule2.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }       
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImageUrl { get; set; }
        public string SmallImageUrl { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HomeNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsAvailable { get; set; }
        public int DealId { get; set; }
        public virtual Deal Deal { get; set; }
        public ICollection<Shift> Shifts { get; set; }
        public ICollection<Vacation> Vacations { get; set; }
        public ICollection <Sugestion> Sugestions { get; set; }
    }
}
