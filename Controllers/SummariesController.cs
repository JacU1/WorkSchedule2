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
    public class SummariesController : Controller
    {
        private readonly WorkScheduleContext _context;

        public SummariesController(WorkScheduleContext context)
        {
            _context = context;
        }

        // GET: Summaries
        public async Task<IActionResult> Index()
        {
            var workScheduleContext = _context.Summaries.Include(s => s.User);
            return View(await workScheduleContext.ToListAsync());
        }

        // GET: Summaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var summary = await _context.Summaries
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (summary == null)
            {
                return NotFound();
            }

            return View(summary);
        }

        // GET: Summaries/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Summaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TotalHoursWorked,Grade,Bonus,Salary,UserId")] Summary summary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(summary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", summary.UserId);
            return View(summary);
        }

        // GET: Summaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var summary = await _context.Summaries.FindAsync(id);
            if (summary == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", summary.UserId);
            return View(summary);
        }

        // POST: Summaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TotalHoursWorked,Grade,Bonus,Salary,UserId")] Summary summary)
        {
            if (id != summary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(summary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SummaryExists(summary.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", summary.UserId);
            return View(summary);
        }

        // GET: Summaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var summary = await _context.Summaries
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (summary == null)
            {
                return NotFound();
            }

            return View(summary);
        }

        // POST: Summaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var summary = await _context.Summaries.FindAsync(id);
            _context.Summaries.Remove(summary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SummaryExists(int id)
        {
            return _context.Summaries.Any(e => e.Id == id);
        }
    }
}
