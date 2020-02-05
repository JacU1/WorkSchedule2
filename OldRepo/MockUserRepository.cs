using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkSchedule2.Models
{
    public class MockUserRepository: IUserRepository
    {
        private readonly IDealRepository _dealRepository = new MockDealRepository();

        public IEnumerable<User> AllUsers =>
            new List<User>
            {
                new User{Id = 1, FirstName="Jacek", LastName="Kowalski", DateOfBirth=DateTime.Parse("27.02.1996"), City="Koszalin",Street="Zwyciestwa 9",HomeNumber="145", PhoneNumber="999-241-243", Email="jacek.kowalski@wp.pl", 
                    ImageUrl="https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypie.jpg", SmallImageUrl="https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypiesmall.jpg", 
                    DealId= 1, IsAdmin=true, IsAvailable=false},
                new User{Id = 2, FirstName="Adam", LastName="Nowak", DateOfBirth=DateTime.Parse("12.06.1997") , City="Poznan",Street="Krakowska 12",HomeNumber="125", PhoneNumber="129-241-243", Email="adam.nowak@wp.pl",
                    ImageUrl="https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecake.jpg", SmallImageUrl="https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecakesmall.jpg",
                    DealId= 2 , IsAdmin=false, IsAvailable=true}
            };

        public IEnumerable<User> AvailableUsers { get;}
        public IEnumerable<User> AdminUsers { get; set; }
        public User GetUserById(int Id)
        {
            return AllUsers.FirstOrDefault(u => u.Id == Id);
        }
    }
}
