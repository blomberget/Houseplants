using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Houseplants.Models;
using Microsoft.AspNetCore.Authorization;

namespace Houseplants.Controllers
{
    [Authorize]
    public class FamiliesController : Controller
    {
        private readonly HouseplantsContext _context;

        public FamiliesController(HouseplantsContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: Family
        public async Task<IActionResult> Index()
        {
            return View(await _context.Family.ToListAsync());
        }

        [AllowAnonymous]
        // GET: Family/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["familyPlant"] = (from p in _context.Plant
                                   where p.Family == id
                                   select p.PlantName).ToList();

            var family = await _context.Family
                .FirstOrDefaultAsync(m => m.FamilyId == id);
            if (family == null)
            {
                return NotFound();
            }

            return View(family);
        }

        // GET: Family/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Family/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FamilyId,FamilyName,FamilyLatin")] Family family)
        {
            if (ModelState.IsValid)
            {
                string familyName = family.FamilyName;

                string familyTitleCase =
                    System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                    ToTitleCase(familyName.ToLower());

                family.FamilyName = familyTitleCase;

                string latin = family.FamilyLatin;

                string latinTitleCase =
                    System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                    ToTitleCase(latin.ToLower());

                family.FamilyLatin = latinTitleCase;

                _context.Add(family);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(family);
        }

        // GET: Family/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await _context.Family.FindAsync(id);
            if (family == null)
            {
                return NotFound();
            }
            return View(family);
        }

        // POST: Family/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FamilyId,FamilyName,FamilyLatin")] Family family)
        {
            if (id != family.FamilyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                string familyName = family.FamilyName;

                string familyTitleCase =
                    System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                    ToTitleCase(familyName.ToLower());

                family.FamilyName = familyTitleCase;

                string latin = family.FamilyLatin;

                string latinTitleCase =
                    System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                    ToTitleCase(latin.ToLower());

                family.FamilyLatin = latinTitleCase;

                try
                {
                    _context.Update(family);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FamilyExists(family.FamilyId))
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
            return View(family);
        }

        // GET: Family/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var family = await _context.Family
                .FirstOrDefaultAsync(m => m.FamilyId == id);
            if (family == null)
            {
                return NotFound();
            }

            return View(family);
        }

        // POST: Family/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var family = await _context.Family.FindAsync(id);
            _context.Family.Remove(family);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FamilyExists(int id)
        {
            return _context.Family.Any(e => e.FamilyId == id);
        }
    }
}
