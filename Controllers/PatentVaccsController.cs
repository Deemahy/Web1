using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web1.Models;

namespace Web1.Controllers
{
    public class PatentVaccsController : Controller
    {
        private readonly Gp2Context _context;

        public PatentVaccsController(Gp2Context context)
        {
            _context = context;
        }

        // GET: PatentVaccs
        public async Task<IActionResult> Index()
        {
            var gp2Context = _context.PatentVaccs.Include(p => p.PatientVac).Include(p => p.PatientVacNavigation);
            return View(await gp2Context.ToListAsync());
        }

        // GET: PatentVaccs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PatentVaccs == null)
            {
                return NotFound();
            }

            var patentVacc = await _context.PatentVaccs
                .Include(p => p.PatientVac)
                .Include(p => p.PatientVacNavigation)
                .FirstOrDefaultAsync(m => m.PatientVacId == id);
            if (patentVacc == null)
            {
                return NotFound();
            }

            return View(patentVacc);
        }

        // GET: PatentVaccs/Create
        public IActionResult Create()
        {
            ViewData["PatientVacId"] = new SelectList(_context.Patients, "PatientId", "PatientId");
            ViewData["PatientVacId"] = new SelectList(_context.Vaccsines, "VaccsineId", "VaccsineId");
            return View();
        }

        // POST: PatentVaccs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientVacId,PatientId,VaccId,VacDay,Width,Headcircumference,Hight")] PatentVacc patentVacc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patentVacc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientVacId"] = new SelectList(_context.Patients, "PatientId", "PatientId", patentVacc.PatientVacId);
            ViewData["PatientVacId"] = new SelectList(_context.Vaccsines, "VaccsineId", "VaccsineId", patentVacc.PatientVacId);
            return View(patentVacc);
        }

        // GET: PatentVaccs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PatentVaccs == null)
            {
                return NotFound();
            }

            var patentVacc = await _context.PatentVaccs.FindAsync(id);
            if (patentVacc == null)
            {
                return NotFound();
            }
            ViewData["PatientVacId"] = new SelectList(_context.Patients, "PatientId", "PatientId", patentVacc.PatientVacId);
            ViewData["PatientVacId"] = new SelectList(_context.Vaccsines, "VaccsineId", "VaccsineId", patentVacc.PatientVacId);
            return View(patentVacc);
        }

        // POST: PatentVaccs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientVacId,PatientId,VaccId,VacDay,Width,Headcircumference,Hight")] PatentVacc patentVacc)
        {
            if (id != patentVacc.PatientVacId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patentVacc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatentVaccExists(patentVacc.PatientVacId))
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
            ViewData["PatientVacId"] = new SelectList(_context.Patients, "PatientId", "PatientId", patentVacc.PatientVacId);
            ViewData["PatientVacId"] = new SelectList(_context.Vaccsines, "VaccsineId", "VaccsineId", patentVacc.PatientVacId);
            return View(patentVacc);
        }

        // GET: PatentVaccs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PatentVaccs == null)
            {
                return NotFound();
            }

            var patentVacc = await _context.PatentVaccs
                .Include(p => p.PatientVac)
                .Include(p => p.PatientVacNavigation)
                .FirstOrDefaultAsync(m => m.PatientVacId == id);
            if (patentVacc == null)
            {
                return NotFound();
            }

            return View(patentVacc);
        }

        // POST: PatentVaccs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PatentVaccs == null)
            {
                return Problem("Entity set 'Gp2Context.PatentVaccs'  is null.");
            }
            var patentVacc = await _context.PatentVaccs.FindAsync(id);
            if (patentVacc != null)
            {
                _context.PatentVaccs.Remove(patentVacc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatentVaccExists(int id)
        {
          return (_context.PatentVaccs?.Any(e => e.PatientVacId == id)).GetValueOrDefault();
        }
    }
}
