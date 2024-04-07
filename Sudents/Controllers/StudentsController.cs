using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sudents.Models;

namespace Sudents.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentContext _context;

        public StudentsController(StudentContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var students = _context.Studs.ToList();
            return View(students);
        }

        public async Task<IActionResult> TotalScore()
        {
            var totalScore = await _context.Studs.SumAsync(s => s.Score);
            ViewBag.TotalScore = totalScore;
            return View();
        }

        public async Task<IActionResult> BestStudents()
        {
            var bestStudents = await _context.Studs.OrderByDescending(s => s.Score).Take(5).ToListAsync();
            return View(bestStudents);
        }

        public async Task<IActionResult> WorstStudents()
        {
            var worstStudents = await _context.Studs.OrderBy(s => s.Score).Take(5).ToListAsync();
            return View(worstStudents);
        }

        public async Task<IActionResult> ExportToTextFile()
        {
            var students = await _context.Studs.ToListAsync();
            var sb = new StringBuilder();

            foreach (var student in students)
            {
                sb.AppendLine($"Имя: {student.Name} Фамилия: {student.Surname} Дисциплина: {student.Subject} Баллы: {student.Score}");
            }

            var fileName = "students.txt";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

            await System.IO.File.WriteAllTextAsync(filePath, sb.ToString());

            ViewBag.Message = $"Список студентов успешно сохранен в файл: {fileName}";

            return View();
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Studs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Subject,Score")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Studs.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Subject,Score")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
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
                    if (!StudentExists(student.Id))
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

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Studs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Studs.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Studs.Remove(student);
            await _context.SaveChangesAsync(); // Save changes here

            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Studs.Any(e => e.Id == id);
        }
    }
}
