using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FARM_ATTENDANCE_SYSTEM.Models;
using FARM_ATTENDANCE_SYSTEM.Data;

namespace FARM_ATTENDANCE_SYSTEM.Controllers
{
    public class RolesController : Controller
    {
        private readonly FarmDbContext _context;

        public RolesController(FarmDbContext context)
        {
            _context = context;
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");

            return View(await _context.Roles.ToListAsync());
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            if (id == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (roles == null)
            {
                return NotFound();
            }

            return View(roles);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,UserGroup")] Roles roles)
        {
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");

            if (ModelState.IsValid)
            {
                // Check if the role being created is "Administrator" and already exists
                if (roles.UserGroup?.Trim().ToLower() == "administrator")
                {
                    bool adminExists = await _context.Roles
                        .AnyAsync(r => r.UserGroup.Trim().ToLower() == "administrator");

                    if (adminExists)
                    {
                        ModelState.AddModelError("UserGroup", "Administrator role already exists.");
                        ViewBag.ErrorMessage = "Administrator role already exists."; // For alert
                        return View(roles);
                    }
                }

                _context.Add(roles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(roles);
        }
        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles.FindAsync(id);
            if (roles == null)
            {
                return NotFound();
            }
            return View(roles);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleId,UserGroup")] Roles roles)
        {
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            if (id != roles.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolesExists(roles.RoleId))
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
            return View(roles);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            if (id == null)
            {
                return NotFound();
            }

            var roles = await _context.Roles
                .FirstOrDefaultAsync(m => m.RoleId == id);
            if (roles == null)
            {
                return NotFound();
            }

            return View(roles);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            var roles = await _context.Roles.FindAsync(id);
            if (roles != null)
            {
                _context.Roles.Remove(roles);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RolesExists(int id)
        {
            return _context.Roles.Any(e => e.RoleId == id);
        }
    }
}
