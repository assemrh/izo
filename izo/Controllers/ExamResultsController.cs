using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using izo.Data;
using izo.Models;

namespace izo.Controllers
{
    public class ExamResultsController : _BaseController
    {
        private readonly DataDbContext _context;

        public ExamResultsController(DataDbContext context)
        {
            _context = context;
        }

        // GET: ExamResults
        public async Task<IActionResult> Index()
        {
            var dataDbContext = _context.ExamResults.Include(e => e.Course).Include(e => e.Student);
            return View(await dataDbContext.ToListAsync());
        }

        public async Task<IActionResult> AvergeScores()
        {
            var dataDbContext = (await _context.ExamResults
                .Include(e => e.Course)
                .Include(e => e.Student).ToListAsync())
                .Where(i => i.Score >= 3)
                .GroupBy(e => new { e.Course, e.Student })
                .Select( x => new Average 
                { 
                    CourseName = x.Key.Course.Name,
                    StudentFullName = x.Key.Student.FullName,
                    Score = x.Average( i => i.Score) 
                }).OrderBy(e => e.StudentFullName);
            return View(dataDbContext);
        }

        // GET: ExamResults/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examResult = await _context.ExamResults
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (examResult == null)
            {
                return NotFound();
            }

            return View(examResult);
        }

        // GET: ExamResults/Create
        public IActionResult Create()
        {
            ViewData["CourseID"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName");
            return View();
        }

        // POST: ExamResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StudentId,CourseID,Score")] ExamResult examResult)
        {
            if (ModelState.IsValid)
            {
                examResult.ID = Guid.NewGuid();
                _context.Add(examResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseID"] = new SelectList(_context.Courses, "Id", "Name", examResult.CourseID);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", examResult.StudentId);
            return View(examResult);
        }

        // GET: ExamResults/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examResult = await _context.ExamResults.FindAsync(id);
            if (examResult == null)
            {
                return NotFound();
            }
            ViewData["CourseID"] = new SelectList(_context.Courses, "Id", "Name", examResult.Course?.Name);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", examResult.StudentId);
            return View(examResult);
        }

        // POST: ExamResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,StudentId,CourseID,Score")] ExamResult examResult)
        {
            if (id != examResult.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamResultExists(examResult.ID))
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
            ViewData["CourseID"] = new SelectList(_context.Courses, "Id", "Name", examResult.CourseID);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", examResult.StudentId);
            return View(examResult);
        }

        // GET: ExamResults/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examResult = await _context.ExamResults
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (examResult == null)
            {
                return NotFound();
            }

            return View(examResult);
        }

        // POST: ExamResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var examResult = await _context.ExamResults.FindAsync(id);
            _context.ExamResults.Remove(examResult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamResultExists(Guid id)
        {
            return _context.ExamResults.Any(e => e.ID == id);
        }
    }
}
