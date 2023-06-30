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
    public class EmergenciesController : Controller
    {
        private readonly Gp2Context _context;

        public EmergenciesController(Gp2Context context)
        {
            _context = context;
        }

        // GET: Emergencies
        public async Task<IActionResult> Index()
        {
            var gp2Context = _context.Emergencies.Include(e => e.Emergency1).Include(e => e.EmergencyNavigation);
            return View(await gp2Context.ToListAsync());
        }

        // GET: Emergencies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Emergencies == null)
            {
                return NotFound();
            }

            var emergency = await _context.Emergencies
                .Include(e => e.Emergency1)
                .Include(e => e.EmergencyNavigation)
                .FirstOrDefaultAsync(m => m.EmergencyId == id);
            if (emergency == null)
            {
                return NotFound();
            }

            return View(emergency);
        }

        // GET: Emergencies/Create
        public IActionResult Create()
        {
            ViewData["EmergencyId"] = new SelectList(_context.Patients, "PatientId", "PatientId");
            ViewData["EmergencyId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId");
            return View();
        }

        // POST: Emergencies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmergencyId,PId,DId,PatientMassages,DoctorResponse")] Emergency emergency)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emergency);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmergencyId"] = new SelectList(_context.Patients, "PatientId", "PatientId", emergency.EmergencyId);
            ViewData["EmergencyId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId", emergency.EmergencyId);
            return View(emergency);
        }

        // GET: Emergencies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Emergencies == null)
            {
                return NotFound();
            }

            var emergency = await _context.Emergencies.FindAsync(id);
            if (emergency == null)
            {
                return NotFound();
            }
            ViewData["EmergencyId"] = new SelectList(_context.Patients, "PatientId", "PatientId", emergency.EmergencyId);
            ViewData["EmergencyId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId", emergency.EmergencyId);
            return View(emergency);
        }

        // POST: Emergencies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmergencyId,PId,DId,PatientMassages,DoctorResponse")] Emergency emergency)
        {
            if (id != emergency.EmergencyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emergency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmergencyExists(emergency.EmergencyId))
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
            ViewData["EmergencyId"] = new SelectList(_context.Patients, "PatientId", "PatientId", emergency.EmergencyId);
            ViewData["EmergencyId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorId", emergency.EmergencyId);
            return View(emergency);
        }

        // GET: Emergencies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Emergencies == null)
            {
                return NotFound();
            }

            var emergency = await _context.Emergencies
                .Include(e => e.Emergency1)
                .Include(e => e.EmergencyNavigation)
                .FirstOrDefaultAsync(m => m.EmergencyId == id);
            if (emergency == null)
            {
                return NotFound();
            }

            return View(emergency);
        }

        // POST: Emergencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Emergencies == null)
            {
                return Problem("Entity set 'Gp2Context.Emergencies'  is null.");
            }
            var emergency = await _context.Emergencies.FindAsync(id);
            if (emergency != null)
            {
                _context.Emergencies.Remove(emergency);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmergencyExists(int id)
        {
          return (_context.Emergencies?.Any(e => e.EmergencyId == id)).GetValueOrDefault();
        }
    }
}
