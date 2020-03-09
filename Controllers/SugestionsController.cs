using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkSchedule2.Data;
using WorkSchedule2.Models;

namespace WorkSchedule2.Controllers
{
    public class SugestionsController : Controller
    {
        private readonly WorkScheduleContext _context;

        public SugestionsController(WorkScheduleContext context)
        {
            _context = context;
        }

        // GET: Sugestions
        [Route("/SugestionsList")]
        public async Task<IActionResult> SugestionsList()
        {
            var workScheduleContext = _context.Sugestions.Include(s => s.User);
            return View(await workScheduleContext.ToListAsync());
        }

        public IActionResult Accepted(int id)
        {
            var sugestionitem = _context.Sugestions.Find(id);
            sugestionitem.type = "approved";
            _context.Update(sugestionitem);
            _context.SaveChanges();

            return Redirect("/SugestionsList");
        }

        public IActionResult Discard(int id)
        {
            var sugetionitem = _context.Sugestions.Find(id);
            sugetionitem.type = "notapproved";
            _context.Update(sugetionitem);
            _context.SaveChanges();


            return Redirect("/SugestionsList");
        }

        // GET: Sugestions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sugestion = await _context.Sugestions
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sugestion == null)
            {
                return NotFound();
            }

            return View(sugestion);
        }

        // GET: Sugestions/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Sugestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,type,Start,End,UserId")] Sugestion sugestion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sugestion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", sugestion.UserId);
            return View(sugestion);
        }

        // GET: Sugestions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sugestion = await _context.Sugestions.FindAsync(id);
            if (sugestion == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", sugestion.UserId);
            return View(sugestion);
        }

        // POST: Sugestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text,type,Start,End,UserId")] Sugestion sugestion)
        {
            if (id != sugestion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sugestion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SugestionExists(sugestion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", sugestion.UserId);
            return View(sugestion);
        }

        // GET: Sugestions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sugestion = await _context.Sugestions
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sugestion == null)
            {
                return NotFound();
            }

            return View(sugestion);
        }

        // POST: Sugestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sugestion = await _context.Sugestions.FindAsync(id);
            _context.Sugestions.Remove(sugestion);
            await _context.SaveChangesAsync();
            return Redirect("/SugestionsList");
        }

        private bool SugestionExists(int id)
        {
            return _context.Sugestions.Any(e => e.Id == id);
        }
    }
}
