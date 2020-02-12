using System;
using System.Collections.Generic;
using System.Linq;
using WorkSchedule2.Data;

namespace WorkSchedule2.Models
{
    public class WebApiEvent
    {
        private static readonly WorkScheduleContext _context;
        public int Id { get; set; }
        public string text { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public int userid { get; set; }

        public static explicit operator WebApiEvent(Shift ev)
        {
            return new WebApiEvent
            {
                Id = ev.Id,
                text = ev.Position,
                start_date = ev.Start.ToString("dd-MM-yyyy HH:mm"),
                end_date = ev.End.ToString("dd-MM-yyyy HH:mm"),
                userid = ev.UserId,
            };
        }

        public static explicit operator Shift(WebApiEvent ev)
        {
            return new Shift
            {
                Id = ev.Id,
                Position = ev.text,
                Start = DateTime.ParseExact(ev.start_date,"dd-MM-yyyy HH:mm",
                    System.Globalization.CultureInfo.InvariantCulture),
                End = DateTime.ParseExact(ev.end_date,"dd-MM-yyyy HH:mm",
                    System.Globalization.CultureInfo.InvariantCulture),
                UserId = ev.userid
            };
        }
    }
}
