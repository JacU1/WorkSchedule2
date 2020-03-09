using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkSchedule2.Data;
using WorkSchedule2.Models;

namespace WorkSchedule2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationsApiController : Controller
    {
        private readonly WorkScheduleContext _context;

        public VacationsApiController(WorkScheduleContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IEnumerable<WebApiEvent> Get()
        {
            var vacationslist = _context.Vacations
                    .ToList()
                    .Select(e => (WebApiEvent)e);

            return vacationslist;
        }


        [HttpPost]
        public IActionResult Post([FromForm] WebApiEvent apiEvent)
        {
            if (HttpContext.Session.GetString("UserAdmin") == "True")
            {
                var loggeduserid = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
                apiEvent.userid = loggeduserid;
                apiEvent.type = "waiting";
                var newEvent = (Vacation)apiEvent;
                newEvent.UserId = loggeduserid;
                newEvent.type = "waiting";
                newEvent.ComplainText = newEvent.ComplainText +"  "+ _context.Users.Where(x => x.Id == loggeduserid).Select(t => t.FirstName + t.LastName).First();
                _context.Vacations.Add(newEvent);
                _context.SaveChanges();

                Ok(new
                {
                    tid = newEvent.Id,
                    action = "inserted"
                });

                return Redirect("/VacationsIndex");
            }
            else
            {
                var loggeduserid = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
                apiEvent.userid = loggeduserid;
                apiEvent.type = "waiting";
                var newEvent = (Vacation)apiEvent;
                newEvent.UserId = loggeduserid;
                newEvent.type = "waiting";
                newEvent.ComplainText = newEvent.ComplainText + "  " + _context.Users.Where(x => x.Id == loggeduserid).Select(t => t.FirstName + t.LastName).First();
                _context.Vacations.Add(newEvent);
                _context.SaveChanges();

                Ok(new
                {
                    tid = newEvent.Id,
                    action = "inserted"
                });

                return Redirect("/VacationsUserIndex");
            }
        }

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



