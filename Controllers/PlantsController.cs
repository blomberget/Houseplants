using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Houseplants.Models;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

namespace Houseplants.Controllers
{
    [Authorize]
    public class PlantsController : Controller
    {
        private readonly HouseplantsContext _context;

        public PlantsController(HouseplantsContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: Plants
        public async Task<IActionResult> Index()
        {
            var houseplantsContext = _context.Plant.Include(p => p.CareNavigation).Include(p => p.FamilyNavigation);
            return View(await houseplantsContext.ToListAsync());
        }

        [AllowAnonymous]
        // GET: Plants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _context.Plant
                .Include(p => p.CareNavigation)
                .Include(p => p.FamilyNavigation)
                .FirstOrDefaultAsync(m => m.PlantId == id);
            if (plant == null)
            {
                return NotFound();
            }

            return View(plant);
        }

        // GET: Plants/Create
        public IActionResult Create()
        {
            string result = "";
            foreach (Care c in _context.Care)
            {
                result += "<tr>" +
                      "<td>" + c.CareId + "</td>" +
                      "<td>" + c.LightNeed + "</td>" +
                      "<td>" + c.WaterNeed + "</td>" +
                      "<td>" + c.NutritionNeed + "</td>" +
                      "</tr>";
            }
            ViewData["CareTable"] = result;
            ViewData["Care"] = new SelectList(_context.Care, "CareId", "CareId");
            ViewData["Family"] = new SelectList(_context.Family, "FamilyId", "FamilyLatin");
            return View();
        }

        // POST: Plants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlantId,PlantName,Latin,Level,Family,Care")] Plant plant)
        {
          
            if (ModelState.IsValid)
            {
                string myText = plant.PlantName;

                string asTitleCase =
                    System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                    ToTitleCase(myText.ToLower());

                plant.PlantName = asTitleCase;

                string latin = plant.Latin;

                string latinTitleCase =
                    System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                    ToTitleCase(latin.ToLower());

                plant.Latin = latinTitleCase;

                _context.Add(plant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Care"] = new SelectList(_context.Care, "CareId", "CareId", plant.Care);
            ViewData["Family"] = new SelectList(_context.Family, "FamilyId", "FamilyLatin", plant.Family);
            return View(plant);
        }

        // GET: Plants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _context.Plant.FindAsync(id);
            if (plant == null)
            {
                return NotFound();
            }

            string result = "";
            foreach (Care c in _context.Care)
            {
                result += "<tr>" +
                      "<td>" + c.CareId + "</td>" +
                      "<td>" + c.LightNeed + "</td>" +
                      "<td>" + c.WaterNeed + "</td>" +
                      "<td>" + c.NutritionNeed + "</td>" +
                      "</tr>";
            }
            ViewData["CareTable"] = result;

            ViewData["Care"] = new SelectList(_context.Care, "CareId", "CareId", plant.Care);
            ViewData["Family"] = new SelectList(_context.Family, "FamilyId", "FamilyLatin", plant.Family);
            return View(plant);
        }

        // POST: Plants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlantId,PlantName,Latin,Level,Family,Care")] Plant plant)
        {
            if (id != plant.PlantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                string myText = plant.PlantName;

                string asTitleCase =
                    System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                    ToTitleCase(myText.ToLower());

                plant.PlantName = asTitleCase;

                string latin = plant.Latin;

                string latinTitleCase =
                    System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                    ToTitleCase(latin.ToLower());

                plant.Latin = latinTitleCase;

                try
                {
                    _context.Update(plant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantExists(plant.PlantId))
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
            ViewData["Care"] = new SelectList(_context.Care, "CareId", "CareId", plant.Care);
            ViewData["Family"] = new SelectList(_context.Family, "FamilyId", "FamilyLatin", plant.Family);
            return View(plant);
        }

        // GET: Plants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _context.Plant
                .Include(p => p.CareNavigation)
                .Include(p => p.FamilyNavigation)
                .FirstOrDefaultAsync(m => m.PlantId == id);
            if (plant == null)
            {
                return NotFound();
            }

            return View(plant);
        }

        // POST: Plants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plant = await _context.Plant.FindAsync(id);
            _context.Plant.Remove(plant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantExists(int id)
        {
            return _context.Plant.Any(e => e.PlantId == id);
        }
    }
}
