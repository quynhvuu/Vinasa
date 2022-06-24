using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NPOI.XSSF.UserModel;
using Vinasa.Data;
using Vinasa.Interfaces;
using Vinasa.Models;

namespace Vinasa.Controllers
{
    public class SeminarParticipantsController : Controller
    {
        private readonly VinasaContext _context;
        private readonly IImportManager _importManager;

        public SeminarParticipantsController(
            VinasaContext context,
            IImportManager importManager)
        {
            _context = context;
            _importManager = importManager;
        }

        // GET: SeminarParticipants
        public async Task<IActionResult> List()
        {
            var seminarParticipants = await _context.SeminarParticipant.ToListAsync();

            return View(seminarParticipants);
        }

        // GET: SeminarParticipants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seminarParticipant = await _context.SeminarParticipant
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seminarParticipant == null)
            {
                return NotFound();
            }

            return View(seminarParticipant);
        }

        // GET: SeminarParticipants/Create
        public IActionResult Create(int? seminarId)
        {
            return View();
        }

        // POST: SeminarParticipants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SeminarId,Name,TaxNumber,Company,Position,Email,PhoneNumber,ProvinceId,JobTitle,Operation,RegistrySeminar,RegistryBusinessMatching,RegistryExhibition,RegistryTicket,CreatedUtc")] SeminarParticipant seminarParticipant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seminarParticipant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            return View(seminarParticipant);
        }

        // GET: SeminarParticipants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seminarParticipant = await _context.SeminarParticipant.FindAsync(id);
            if (seminarParticipant == null)
            {
                return NotFound();
            }
            return View(seminarParticipant);
        }

        // POST: SeminarParticipants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TaxNumber,Company,Position,Email,PhoneNumber,ProvinceId,JobTitle,Operation,RegistrySeminar,RegistryBusinessMatching,RegistryExhibition,RegistryTicket,CreatedUtc")] SeminarParticipant seminarParticipant)
        {
            if (id != seminarParticipant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seminarParticipant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeminarParticipantExists(seminarParticipant.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(List));
            }
            return View(seminarParticipant);
        }

        // POST: SeminarParticipants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int seminarId = -1)
        {
            var seminarParticipant = await _context.SeminarParticipant.FindAsync(id);
            _context.SeminarParticipant.Remove(seminarParticipant);
            await _context.SaveChangesAsync();
            if(seminarId > 0)
                return RedirectToAction("Details", "Seminars", new { id = seminarId });
            else
                return RedirectToAction(nameof(List));

        }

        public ActionResult DeleteSelected(int id, int seminarId = -1)
        {
            var model = _context.SeminarParticipant.Where(m => m.Id == id).FirstOrDefault();
            ViewBag.SeminarId = seminarId;
            return PartialView("_DeleteSelected", model);
        }

        private bool SeminarParticipantExists(int id)
        {
            return _context.SeminarParticipant.Any(e => e.Id == id);
        }

        
    }
}
