using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WorkSchedule2.Data;
using WorkSchedule2.Models;

namespace WorkSchedule2.Controllers
{
    public class UsersController : Controller
    {
        private readonly WorkScheduleContext _context;

        public UsersController(WorkScheduleContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AdminUsersList()
        {
            return View(await _context.Users.ToListAsync());
        }

        [Route("/Login")]
        public IActionResult Login()
        {
            ViewData["Message"] = "Login Page";
            return View("Login");
        }

        public IActionResult LoginAction(string login, string password)
        {
            var userlogin = _context.Users.ToList().Select(x => x.Login).ToList();
            var passwordlist = _context.Users.ToList().Select(x => x.Password).ToList();

            if (userlogin.Contains(login) && passwordlist.Contains(password))
            {
                HttpContext.Session.SetString("UserLogin", login);
                var userloginlist = _context.Users.ToList().First(x => x.Login == login);
                var userid = userloginlist.Id;
                var userAdminbool = userloginlist.IsAdmin;
                var userAdmin = userAdminbool.ToString();
                var userdealid = userloginlist.DealId;
                HttpContext.Session.SetInt32("UserDealId", userdealid);
                HttpContext.Session.SetString("UserAdmin", userAdmin);
                HttpContext.Session.SetInt32("UserId", userid);
                if(userAdminbool == false)
                {
                    return Redirect("/UserIndex");
                }
                else
                {
                    return Redirect("/AdminIndex");
                }

            }
            else
            {
                return Redirect("Users/Login");
            }
        }
        [Route("/AdminIndex")]
        public IActionResult AdminIndex()
        {
            return View();
        }
        [Route("/UserIndex")]
        public IActionResult UserIndex()
        {
            return View();
        }
        
        public async Task<IActionResult> UserDetails()
        {
            var id = HttpContext.Session.GetInt32("UserId");
            //ViewBag["UserID"] = id;

            var user = await _context.Users
                .Include(u => u.Deal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View("UserDetails",user);
        }

        


        // GET: Users/Details/5
        public async Task<IActionResult> AdminDetailsList(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Deal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View("AdminDetailsList",user);
        }

        // GET: Users/Create
        public IActionResult CreateNew()
        {
            ViewData["DealId"] = new SelectList(_context.Deals, "Id", "Id");                // WAŻNE ZAPAMIĘTAJ JAK SIĘ TO ROBI PRZYDA SIĘ DO DROPDOWNA DO SHIFTS
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Login,Password,FirstName,LastName,DateOfBirth,ImageUrl,SmallImageUrl,City,Street,HomeNumber,Email,PhoneNumber,IsAdmin,IsAvailable,DealId")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DealId"] = new SelectList(_context.Deals, "Id", "Id", user.DealId);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> UserEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["DealId"] = new SelectList(_context.Deals, "Id", "Id", user.DealId);
            return View(user);
        }
        public async Task<IActionResult> AdminEditView(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["DealType"] = new SelectList(_context.Deals.Select(x=>x.DealType), "Id", "Id", user);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Login,Password,FirstName,LastName,DateOfBirth,ImageUrl,SmallImageUrl,City,Street,HomeNumber,Email,PhoneNumber,IsAdmin,IsAvailable,DealId")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AdminUsersList));
            }
            ViewData["DealId"] = new SelectList(_context.Deals, "Id", "Id", user.DealId);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Deal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
