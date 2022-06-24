using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vinasa.Data;
using Vinasa.Interfaces;
using Vinasa.Models;

namespace Vinasa.Controllers
{
    public class SeminarsController : Controller
    {
        private readonly VinasaContext _context;
        private readonly IImportManager _importManager;

        public SeminarsController(
            VinasaContext context,
            IImportManager importManager)
        {
            _context = context;
            _importManager = importManager;

        }

        // GET: Seminars
        public async Task<IActionResult> List()
        {
            return View(await _context.Seminar.ToListAsync());
        }

        // GET: SeminarParticipants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seminar = await _context.Seminar
                .FirstOrDefaultAsync(m => m.Id == id);

            if (seminar == null)
            {
                return NotFound();
            }

            seminar.SeminarParticipants = await _context.SeminarParticipant.Where(m => m.SeminarId == seminar.Id).ToListAsync();
            
            return View(seminar);
        }

        // GET: Seminars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Seminars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,OpenDate,Address,CreatedUtc")] Seminar seminar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seminar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            return View(seminar);
        }

        // GET: Seminars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seminar = await _context.Seminar.FindAsync(id);
            if (seminar == null)
            {
                return NotFound();
            }
            return View(seminar);
        }

        // POST: Seminars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,OpenDate,Address,CreatedUtc")] Seminar seminar)
        {
            if (id != seminar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seminar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeminarExists(seminar.Id))
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
            return View(seminar);
        }

        // POST: Seminars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            var seminar = await _context.Seminar.FindAsync(id);
            _context.Seminar.Remove(seminar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool SeminarExists(int id)
        {
            return _context.Seminar.Any(e => e.Id == id);
        }

        public ActionResult DeleteSelected(int id)
        {
            var model = _context.Seminar.Where(m => m.Id == id).FirstOrDefault();
            return PartialView("_DeleteSelected", model);
        }

        [HttpPost]
        public async Task<IActionResult> ImportExcel(int? id, IFormFile importexcelfile)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                if (importexcelfile != null && importexcelfile.Length > 0)
                {
                    await _importManager.ImportSeminarParticipantFromXlsx((int)id, importexcelfile.OpenReadStream());
                }
                else
                {
                    return RedirectToAction(nameof(Details), new {id = id});
                }
                return RedirectToAction(nameof(Details), new { id = id });
            }
            catch (Exception exc)
            {
                return RedirectToAction(nameof(Details), new { id = id, erorr = exc });
            }
        }
    }
}
