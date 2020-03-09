using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkSchedule2.Data;
using WorkSchedule2.Models;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace WorkSchedule2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShiftsController : Controller
    {
        private readonly WorkScheduleContext _context;

        public ShiftsController(WorkScheduleContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IEnumerable<WebApiEvent> Get()
        {
            var userid = HttpContext.Session.GetInt32("UserId");

            var shiftlist = _context.Shifts
                   .ToList()
                   .Select(e => (WebApiEvent)e);

            var sugestionlist = _context.Sugestions
                .ToList()
                .Select(e => (WebApiEvent)e);

            var allitemslist = shiftlist.Concat(sugestionlist).ToList();

            return allitemslist;
        }

        [HttpGet("{id}")]
        public WebApiEvent Get(int id)
        {
            return (WebApiEvent)_context
                .Shifts
                .Find(id);
        }

        // POST api/events
        [HttpPost]
        public IActionResult Post([FromForm] WebApiEvent apiEvent)
        {

            if (HttpContext.Session.GetString("UserAdmin") == "True")
            {
                var username = apiEvent.user_name;
                var userlastname = apiEvent.user_last_name;
                var userid = _context.Users.Where(x => x.FirstName == username && x.LastName == userlastname).Select(y => y.Id).First();
                
                DateTime start = DateTime.Parse(apiEvent.start_date);
                DateTime end = DateTime.Parse(apiEvent.end_date);

                int hourscount = 0;

                for (var i = start; i < end; i = i.AddHours(1))
                {
                    
                    if (i.TimeOfDay.Hours >= 9 && i.TimeOfDay.Hours < 17)
                    {
                        hourscount++;
                    }
                    
                }

                apiEvent.text = apiEvent.text + "  " + apiEvent.user_name + " " + apiEvent.user_last_name +" "+ "Manager"+ " " + apiEvent.supervisor_name;
                apiEvent.userid = userid;
                var newEvent = (Shift)apiEvent;
                newEvent.UserId = userid;
                newEvent.Hoursworked = hourscount;
                _context.Shifts.Add(newEvent);
                _context.SaveChanges();

                Ok(new
                {
                    tid = newEvent.Id,
                    action = "inserted",
                });

                return Redirect("/AdminIndex");

            }
            else if (HttpContext.Session.GetString("UserAdmin") == "False")
            {
                var loggeduserid = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
                apiEvent.userid = loggeduserid;
                apiEvent.type = "waiting";
                var newEvent = (Sugestion)apiEvent;
                newEvent.UserId = loggeduserid;
                newEvent.type = "waiting";
                _context.Sugestions.Add(newEvent);
                _context.SaveChanges();

                Ok(new
                {
                    tid = newEvent.Id,
                    action = "inserted"
                });
                return Redirect("/UserIndex");
            }
            else
            {
                return Redirect("/UserIndex");
            }
        }


        // PUT api/events/5
        [HttpPut]
        public ObjectResult Put([FromForm] WebApiEvent apiEvent)
        {
            var updatedEvent = (Shift)apiEvent;
            var dbEvent = _context.Shifts.Find(apiEvent.Id);
            dbEvent.Position = updatedEvent.Position;
            dbEvent.Start = updatedEvent.Start;
            dbEvent.End = updatedEvent.End;
            _context.SaveChanges();

            return Ok(new
            {
                action = "updated"
            });
        }

        // DELETE api/events/5
        [HttpDelete("{id}")]
        public ObjectResult DeleteEvent(int id)
        {
            var e = _context.Shifts.Find(id);
            if (e != null)
            {
                _context.Shifts.Remove(e);
                _context.SaveChanges();
            }

            return Ok(new
            {
                action = "deleted"
            });
        }

    }
}
    


