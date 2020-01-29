using System.Collections.Generic;

namespace WorkSchedule2.Models
{
    public class Deal
    {
        public int Id { get; set; }
        public string Role { get; set; }    //serwisant , kucharz 
        public string DealType { get; set; }    // umowa o pracę , zlecenie 
        public decimal PayPerHour { get; set; }     //stawka godzinowa 
        public decimal PayPerMonth { get; set; }    // stawka miesięczna na 168 h przepracowane (umowa o pracę)
        public List<User> Users { get; set; }
    }
}
