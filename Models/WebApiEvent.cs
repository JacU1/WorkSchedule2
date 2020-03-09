using System;
using System.Collections.Generic;
using System.Linq;
using WorkSchedule2.Data;

namespace WorkSchedule2.Models
{
    public class WebApiEvent
    {
        public int Id { get; set; }
        public string text { get; set; }
        public string supervisor_name { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string user_name { get; set; }
        public string user_last_name { get; set; }
        public int userid { get; set; }
        public string type { get; set; }

        public static explicit operator WebApiEvent(Shift ev)
        {
            return new WebApiEvent
            {
                Id = ev.Id,
                text = ev.Position,
                start_date = ev.Start.ToString("yyyy-MM-ddTHH:mm"),
                end_date = ev.End.ToString("yyyy-MM-ddTHH:mm"),
                supervisor_name = ev.Supervisor,
                userid = ev.UserId,
                type = ev.type
            };
        }


        public static explicit operator Shift(WebApiEvent ev)
        {
            return new Shift
            {
                Id = ev.Id,
                Position = ev.text,
                Start = DateTime.ParseExact(ev.start_date, "yyyy-MM-ddTHH:mm",
                    System.Globalization.CultureInfo.InvariantCulture),
                End = DateTime.ParseExact(ev.end_date, "yyyy-MM-ddTHH:mm",
                    System.Globalization.CultureInfo.InvariantCulture),
                Supervisor = ev.supervisor_name,
                UserId = ev.userid,
                type = ev.type
            };
        }

        public static explicit operator WebApiEvent(Vacation ev)
        {
            return new WebApiEvent
            {
                Id = ev.Id,
                start_date = ev.Start.ToString("yyyy-MM-ddTHH:mm"),
                end_date = ev.End.ToString("yyyy-MM-ddTHH:mm"),
                userid = ev.UserId,
                text = ev.ComplainText,
                type = ev.type
            };
        }

        public static explicit operator Vacation(WebApiEvent ev)
        {
            return new Vacation
            {
                Id = ev.Id,
                Start = DateTime.ParseExact(ev.start_date, "yyyy-MM-ddTHH:mm",
                System.Globalization.CultureInfo.InvariantCulture),
                End = DateTime.ParseExact(ev.end_date, "yyyy-MM-ddTHH:mm",
                System.Globalization.CultureInfo.InvariantCulture),
                UserId = ev.userid,
                ComplainText = ev.text,
                type = ev.type
            };
        }

        public static explicit operator WebApiEvent(Sugestion ev)
        {
            return new WebApiEvent
            {
                Id = ev.Id,
                start_date = ev.Start.ToString("yyyy-MM-ddTHH:mm"),
                end_date = ev.End.ToString("yyyy-MM-ddTHH:mm"),
                userid = ev.UserId,
                text = ev.Text,
                type = ev.type
            };
        }

        public static explicit operator Sugestion(WebApiEvent ev)
        {
            return new Sugestion
            {
                Id = ev.Id,
                Start = DateTime.ParseExact(ev.start_date, "yyyy-MM-ddTHH:mm",
                System.Globalization.CultureInfo.InvariantCulture),
                End = DateTime.ParseExact(ev.end_date, "yyyy-MM-ddTHH:mm",
                System.Globalization.CultureInfo.InvariantCulture),
                UserId = ev.userid,
                Text = ev.text,
                type = ev.type
            };
        }
    }
}
