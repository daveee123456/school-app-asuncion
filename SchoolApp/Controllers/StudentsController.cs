
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Data;
using SchoolApp.Models;

namespace SchoolApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolAppContext _context;

        public StudentsController(SchoolAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Student.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("StudentId,StudentNo,Name,Email,ConfirmEmail,Phone,Age,BirthDate")]
            Student student)
        {
            if (student.BirthDate.HasValue &&
                student.BirthDate.Value.Date > DateTime.Today)
            {
                ModelState.AddModelError(
                    nameof(student.BirthDate),
                    "Birth Date cannot be in the future.");
            }

            if (!string.IsNullOrWhiteSpace(student.Email) &&
                await _context.Student.AnyAsync(s =>
                    s.Email != null &&
                    s.Email.ToLower() == student.Email.ToLower()))
            {
                ModelState.AddModelError(
                    nameof(student.Email),
                    "This email address is already registered.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(student);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            student.ConfirmEmail = student.Email;

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("StudentId,StudentNo,Name,Email,ConfirmEmail,Phone,Age,BirthDate")]
            Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (student.BirthDate.HasValue &&
                student.BirthDate.Value.Date > DateTime.Today)
            {
                ModelState.AddModelError(
                    nameof(student.BirthDate),
                    "Birth Date cannot be in the future.");
            }

            if (!string.IsNullOrWhiteSpace(student.Email) &&
                await _context.Student.AnyAsync(s =>
                    s.StudentId != student.StudentId &&
                    s.Email != null &&
                    s.Email.ToLower() == student.Email.ToLower()))
            {
                ModelState.AddModelError(
                    nameof(student.Email),
                    "This email address is already registered.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
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

            return View(student);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Student.FindAsync(id);

            if (student != null)
            {
                _context.Student.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.StudentId == id);
        }
    }
}
