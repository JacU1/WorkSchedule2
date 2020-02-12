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
    public class ShiftsController : ControllerBase
    {
        private readonly WorkScheduleContext _context;

        public ShiftsController(WorkScheduleContext context)
        {
            _context = context;
        }

        // GET SHIFTS
        
        [HttpGet]
        public IEnumerable<WebApiEvent> Get()
        {
            var userid = HttpContext.Session.GetInt32("UserId");

            return _context.Shifts.Where(x => x.UserId == userid)
                    .ToList()
                    .Select(e => (WebApiEvent)e);
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
        public ObjectResult Post([FromForm] WebApiEvent apiEvent)
        {
            var newEvent = (Shift)apiEvent;

            _context.Shifts.Add(newEvent);
            _context.SaveChanges();

            return Ok(new
            {
                tid = newEvent.Id,
                action = "inserted",
                RedirectResult = Redirect("AdminIndex")
            }); 
            ;
        }

        // PUT api/events/5
        [HttpPut("{id}")]
        public ObjectResult Put(int id, [FromForm] WebApiEvent apiEvent)
        {
            var updatedEvent = (Shift)apiEvent;
            var dbEvent = _context.Shifts.Find(id);
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
    


