using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkSchedule2.Models
{
    public class WebApiEvent
    {
        public int id { get; set; }
        public string text { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public int userid { get; set; }

        public static explicit operator WebApiEvent(Shift ev)
        {
            return new WebApiEvent
            {
                id = ev.Id,
                text = ev.Position,
                start_date = ev.Start.ToString("dd-MM-yyyy HH:mm"),
                end_date = ev.End.ToString("dd-MM-yyyy HH:mm"),
                userid = ev.UserId
            };
        }
        public static explicit operator Shift(WebApiEvent ev)
        {
            return new Shift
            {
                Id = ev.id,
                Position = ev.text,
                Start = DateTime.Parse(ev.start_date,
                    System.Globalization.CultureInfo.InvariantCulture),
                End = DateTime.Parse(ev.end_date,
                    System.Globalization.CultureInfo.InvariantCulture),
                UserId = ev.userid
            };
        }
    }
}
