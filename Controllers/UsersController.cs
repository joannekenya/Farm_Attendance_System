using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FARM_ATTENDANCE_SYSTEM.Models;
using FARM_ATTENDANCE_SYSTEM.Data;
using Microsoft.AspNetCore.Identity;

namespace FARM_ATTENDANCE_SYSTEM.Controllers
{
   
        public class UsersController : Controller
        {
            private readonly FarmDbContext _context;

            public UsersController(FarmDbContext context)
            {
                _context = context;
            }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            return View(await _context.Users.ToListAsync());
        }
      
        // GET: Users
        public async Task<IActionResult> Index1()
            {
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            return View(await _context.Users.ToListAsync());


            }

            // GET: Users/Details/5
            public async Task<IActionResult> Details(int? id)
            {
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            if (id == null)
                {
                    return NotFound();
                }

                var users = await _context.Users
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (users == null)
                {
                    return NotFound();
                }

                return View(users);
            }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user, string Password, string ConfirmPassword)
        {
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
                return View(user);
            }

            // Normalize input
            var inputFullName = user.FullName?.Trim().ToLower();
            var inputEmail = user.Email?.Trim().ToLower();
            var inputUsername = user.UserName?.Trim().ToLower();
            var inputPhone = user.PhoneNo?.Trim();

            bool userExists = _context.Users.Any(u =>
                u.FullName.ToLower() == inputFullName &&
                u.Email.ToLower() == inputEmail &&
                u.UserName.ToLower() == inputUsername &&
                u.PhoneNo == inputPhone
            );

            if (userExists)
            {
                ModelState.AddModelError("", "A user with the same details already exists.");
                return View(user);
            }

            // Validate password input
            if (string.IsNullOrWhiteSpace(Password))
            {
                ModelState.AddModelError("", "Password is required.");
                return View(user);
            }

            if (Password != ConfirmPassword)
            {
                ModelState.AddModelError("", "Password and Confirm Password do not match.");
                return View(user);
            }

            try
            {
                // Hash the password
                var passwordHasher = new PasswordHasher<User>();
                user.PasswordHash = passwordHasher.HashPassword(user, Password);

                // Set default values
                user.UserGroup = "User";
                user.EmailConfirmed = false;

                // Save user
                _context.Add(user);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "User created successfully.";
                return RedirectToAction("Index", "Home");
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"DB Error: {dbEx.Message}");
                ModelState.AddModelError("", "Database error occurred while creating the user.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                ModelState.AddModelError("", "An unexpected error occurred while creating the user.");
            }

            return View(user);
        }



        // GET: Users/Edit/5
        public IActionResult Edit(int id)
        {
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            ViewBag.UserGroups = _context.Roles
        .Select(r => new { Id = r.RoleId, Name = r.UserGroup })
        .ToList();
            return View(user);
        }



        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,UserGroup,Email,UserName,PhoneNo,PasswordHash")] User users)
        {
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index1));
            }

            ViewBag.UserGroups = _context.Roles
     .Select(r => new { Id = r.RoleId, Name = r.UserGroup })
     .ToList();
            return View(users);
        }



        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");

            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            // Check if user is administrator
            if (IsAdministrator(user))
            {
                TempData["ErrorMessage"] = "Administrator users cannot be deleted.";
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // Updated helper method with null safety
        private bool IsAdministrator(User? user)
        {
            return user?.UserGroup?.Equals("Administrator", StringComparison.OrdinalIgnoreCase) ?? false;
        }

        // Updated POST action with null check
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            // Fresh check with null handling
            var freshUserData = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (freshUserData == null || IsAdministrator(freshUserData))
            {
                TempData["ErrorMessage"] = "Administrator users cannot be deleted.";
                return RedirectToAction(nameof(Index));
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "User deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
