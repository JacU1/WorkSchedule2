using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkSchedule2.Data;
using WorkSchedule2.Models;
using PdfSharp;
using PdfSharp.Charting;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Internal;
using PdfSharp.Pdf;
using PdfSharp.SharpZipLib;
using System.Diagnostics;

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

        [Route("/MySummaries")]
        public IActionResult MySummaries()
        {
            var userid = HttpContext.Session.GetInt32("UserId");
            var mysummaries = _context.Summaries.Where(x => x.UserId == userid);
            return View(mysummaries);
        }

        public IActionResult GeneratePDF(int id)
        {
            var userid = HttpContext.Session.GetInt32("UserId");

            var selecteditem = _context.Summaries.Find(id);
            var userfirstname = _context.Users.Where(x => x.Id == userid).Select(t => t.FirstName).First();
            var userlastname = _context.Users.Where(x => x.Id == userid).Select(t => t.LastName).First();


            PdfDocument document = new PdfDocument();
            document.Info.Title = "Work report";

            PdfPage page = new PdfPage();
            document.AddPage(page);

            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Arial", 11, XFontStyle.Regular);

            gfx.DrawString(userfirstname + " " + userlastname + Environment.NewLine + "Total hours worked:  " + selecteditem.TotalHoursWorked.ToString()
                + Environment.NewLine + "Grade:  " + selecteditem.Grade.ToString()
                + Environment.NewLine + "Bonus:  " + selecteditem.Bonus.ToString() 
                + Environment.NewLine + "Salary: " + selecteditem.Salary.ToString(), 
                font, XBrushes.Black, 
                new XRect(0, 0, page.Width, page.Height), XStringFormats.TopCenter);

            string filename = "Raport.pdf";
            document.Save("C:\\Users\\jacun\\Desktop\\" + filename);

            return Redirect("/MySummaries");
        }

        public IActionResult CreateRaportView()
        {
            var userid = HttpContext.Session.GetInt32("UserId");

            var userdealid = _context.Users.Where(r => r.Id == userid).Select(w => w.DealId).First();
            var hoursworked = _context.Shifts.Where(x => x.UserId == userid && x.Start.Month == DateTime.Now.Month && x.End.Month == DateTime.Now.Month).Sum(y => y.Hoursworked);
            var dealsalary = _context.Deals.Where(u => u.Id == userdealid).Select(s => s.PayPerHour).First();

            Summary summary = new Summary();
            summary.UserId = Convert.ToInt32(userid);
            summary.TotalHoursWorked = hoursworked;
            summary.Salary = hoursworked * dealsalary;

            _context.Add(summary);
            _context.SaveChanges();
            return View();
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
        public IActionResult CreateView()
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
