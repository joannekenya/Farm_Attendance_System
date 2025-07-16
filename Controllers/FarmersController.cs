using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FARM_ATTENDANCE_SYSTEM.Models;
using FARM_ATTENDANCE_SYSTEM.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using OfficeOpenXml;
using Microsoft.Extensions.Logging;

namespace FARM_ATTENDANCE_SYSTEM.Controllers
{
    public class FarmersController : Controller
    {
        private readonly FarmDbContext _context;
        private readonly ILogger<FarmersController> _logger;

        public FarmersController(FarmDbContext context, ILogger<FarmersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Farmers
        public async Task<IActionResult> Index(string searchName, string searchIDNO)
        {
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            ViewBag.SearchName = searchName;
            ViewBag.SearchIDNO = searchIDNO;


            var farmersQuery = _context.Farmers.Where(k => k.Audit == false);

            if (!string.IsNullOrEmpty(searchName))
            {
                farmersQuery = farmersQuery.Where(f => f.FarmerName.Contains(searchName));
            }

            if (!string.IsNullOrEmpty(searchIDNO))
            {
                farmersQuery = farmersQuery.Where(f => f.IDNO.Contains(searchIDNO));
            }

            General general = new General
            {
                Farmers = await farmersQuery.ToListAsync(),
                Locations = await _context.Locations.ToListAsync()
            };

            return View(general);
        }


        // GET: Farmers

        public async Task<IActionResult> Index1()
        {
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            General general = new General
            {
                Farmers = await _context.Farmers.Where(k=> k.Audit ==false).ToListAsync(),
                Locations = await _context.Locations.ToListAsync(),
            };


            return View(general);
        }




        // GET: Farmers
        public async Task<IActionResult> Index2()
        {
            ViewBag.county_name = _context.Locations.ToList();

            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            General general = new General
            {
                Farmers = await _context.Farmers.OrderByDescending(k => k.CreatedAt).ToListAsync(),
                Locations = await _context.Locations.ToListAsync(),
            };

            return View(general);
        }

        [HttpPost]
        public async Task<IActionResult> Index2(string County)
        {
            var farmersQuery = _context.Farmers.AsQueryable();

            if (!string.IsNullOrEmpty(County))
            {
                farmersQuery = farmersQuery.Where(k => k.SubCounties == County);
            }

            General general = new General
            {
                Farmers = await farmersQuery.ToListAsync(),
                Locations = await _context.Locations.ToListAsync(),
            };


            ViewBag.county_name = _context.Locations.Select(l => l.county_name).Distinct().ToList();
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            return View(general);
        }



        // GET: Farmers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            General general = new General();

            if (id == null)
            {
                return NotFound();
            }

            var farmers = await _context.Farmers
                .FirstOrDefaultAsync(m => m.FarmerId == id);
            if (farmers == null)
            {
                return NotFound();
            }
            // Fetch distinct locations from the database
            var locations = _context.Locations.ToList();
            ViewBag.county_name = _context.Locations.Select(l => l.county_name).Distinct().ToList();
            ViewBag.sub_counties = _context.Locations.Select(l => l.sub_counties).Distinct().ToList();



            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");
            return View(farmers);
        }

        // GET: Farmers/Create
        public IActionResult Create()
        {
            // Fetch farmers or create a sample list if needed
            var farmers = _context.Farmers.ToList(); // Example query to fetch farmers
            ViewBag.Farmers = farmers;

            // Other ViewBag assignments
            ViewBag.county_name = _context.Locations.Select(l => l.county_name).Distinct().ToList();
            ViewBag.sub_counties = _context.Locations.Select(l => l.sub_counties).Distinct().ToList();
            ViewBag.UserRole = HttpContext.Session.GetString("UserRole");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Create(Farmers farmer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    farmer.CreatedAt = DateTime.Now;
                    _context.Farmers.Add(farmer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                // Log validation errors
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                _logger.LogWarning("Validation errors: {@Errors}", errors);

                // Repopulate dropdowns
                ViewBag.county_name = _context.Locations.Select(l => l.county_name).Distinct().ToList();
                ViewBag.sub_counties = _context.Locations.Select(l => l.sub_counties).Distinct().ToList();
                return View(farmer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating farmer");
                ModelState.AddModelError("", "An error occurred while saving.");
                ViewBag.county_name = _context.Locations.Select(l => l.county_name).Distinct().ToList();
                ViewBag.sub_counties = _context.Locations.Select(l => l.sub_counties).Distinct().ToList();
                return View(farmer);
            }
        }


        // GET: farmers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmers = await _context.Farmers.FindAsync(id);

            if (farmers == null)
            {
                return NotFound();
            }

            var locations = _context.Locations.ToList();

            ViewBag.county_name = _context.Locations.Select(l => l.county_name).Distinct().ToList();
            ViewBag.sub_counties = _context.Locations.Select(l => l.sub_counties).Distinct().ToList();



            return View(farmers);
        }

        // POST:farmers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FarmerId,FarmerName,FPhoneNo,IDNO,GENDER,FPO_COOP_Group,AMOUNT,AGE,VENUE,County,SubCounties,FIELDCOORDINATOR")] Farmers farmers, DateTime date)
        {
            if (id != farmers.FarmerId)
            {
                return NotFound();
            }

            var existingFarmer = await _context.Farmers.FindAsync(id);
            if (existingFarmer == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update only the necessary fields in existingFarmer
                    existingFarmer.CreatedAt = date;

                    existingFarmer.FarmerName = farmers.FarmerName;
                    existingFarmer.FPhoneNo = farmers.FPhoneNo;
                    existingFarmer.IDNO = farmers.IDNO;
                    existingFarmer.GENDER = farmers.GENDER;
                    existingFarmer.FPO_COOP_Group = farmers.FPO_COOP_Group;
                    existingFarmer.AMOUNT = farmers.AMOUNT;
                    existingFarmer.AGE = farmers.AGE;
                    existingFarmer.VENUE = farmers.VENUE;
                    existingFarmer.FIELDCOORDINATOR = farmers.FIELDCOORDINATOR;
                    existingFarmer.County = farmers.County;
                    existingFarmer.SubCounties = farmers.SubCounties;


                    _context.Update(existingFarmer);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FarmerExists(farmers.FarmerId))
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

            var locations = _context.Locations.ToList();
ViewBag.county_name = _context.Locations.ToList();
            ViewBag.sub_counties = _context.Locations.ToList();

            return View(farmers);
        }


        // GET: Farmers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmer = await _context.Farmers
                .FirstOrDefaultAsync(m => m.FarmerId == id);
            if (farmer == null)
            {
                return NotFound();
            }

            return View(farmer);
        }

        // POST: Farmers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var farmer = await _context.Farmers.FindAsync(id);
            if (farmer != null)
            {
                _context.Farmers.Remove(farmer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> ImportExcel([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["ErrorMessage"] = "Please select an Excel file";
                return RedirectToAction(nameof(Index));
            }

            // Validate file extension
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (fileExtension != ".xlsx" && fileExtension != ".xls")
            {
                TempData["ErrorMessage"] = "Only Excel files (.xlsx, .xls) are allowed";
                return RedirectToAction(nameof(Index));
            }

            var farmers = new List<Farmers>();
            var errorMessages = new List<string>();
            int rowNumber = 2; // Start from row 2 (assuming row 1 is header)

            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension?.Rows ?? 0;

                        if (rowCount == 0)
                        {
                            TempData["ErrorMessage"] = "The Excel file is empty";
                            return RedirectToAction(nameof(Index));
                        }

                        for (; rowNumber <= rowCount; rowNumber++)
                        {
                            try
                            {
                                var farmer = new Farmers
                                {
                                    FarmerName = worksheet.Cells[rowNumber, 1].Value?.ToString()?.Trim() ?? string.Empty,
                                    FPhoneNo = worksheet.Cells[rowNumber, 2].Value?.ToString()?.Trim() ?? string.Empty,
                                    IDNO = worksheet.Cells[rowNumber, 3].Value?.ToString()?.Trim() ?? string.Empty,
                                    GENDER = worksheet.Cells[rowNumber, 4].Value?.ToString()?.Trim() ?? string.Empty,
                                    FPO_COOP_Group = worksheet.Cells[rowNumber, 5].Value?.ToString()?.Trim() ?? string.Empty,
                                    VENUE = worksheet.Cells[rowNumber, 8].Value?.ToString()?.Trim() ?? string.Empty,
                                    FIELDCOORDINATOR = worksheet.Cells[rowNumber, 9].Value?.ToString()?.Trim() ?? string.Empty,
                                    County = worksheet.Cells[rowNumber, 10].Value?.ToString()?.Trim() ?? string.Empty,
                                    SubCounties = worksheet.Cells[rowNumber, 11].Value?.ToString()?.Trim() ?? string.Empty,
                                    CreatedAt = DateTime.Now
                                };

                                // Validate required fields
                                if (string.IsNullOrEmpty(farmer.FarmerName))
                                    throw new Exception("Farmer Name is required");
                                if (string.IsNullOrEmpty(farmer.IDNO))
                                    throw new Exception("ID Number is required");

                                // Parse numeric values with error handling
                                if (decimal.TryParse(worksheet.Cells[rowNumber, 6].Value?.ToString(), out var amount))
                                    farmer.AMOUNT = amount;
                                else
                                    throw new Exception("Invalid AMOUNT value");

                                if (int.TryParse(worksheet.Cells[rowNumber, 7].Value?.ToString(), out var age))
                                    farmer.AGE = age;
                                else
                                    throw new Exception("Invalid AGE value");

                                farmers.Add(farmer);
                            }
                            catch (Exception ex)
                            {
                                errorMessages.Add($"Row {rowNumber}: {ex.Message}");
                            }
                        }
                    }
                }

                // Only save if we have valid records
                if (farmers.Any())
                {
                    // Batch save for better performance (adjust batch size as needed)
                    const int batchSize = 100;
                    for (int i = 0; i < farmers.Count; i += batchSize)
                    {
                        var batch = farmers.Skip(i).Take(batchSize);
                        await _context.Farmers.AddRangeAsync(batch);
                        await _context.SaveChangesAsync();
                    }

                    TempData["SuccessMessage"] = $"Successfully imported {farmers.Count} farmers";
                }

                if (errorMessages.Any())
                {
                    TempData["ImportErrors"] = errorMessages;
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error importing file: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }


        private bool FarmerExists(int id)
        {
            return _context.Farmers.Any(e => e.FarmerId == id);
        }
    }
}
