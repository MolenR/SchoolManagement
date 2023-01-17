using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.MVC.Data;

namespace SchoolManagement.MVC.Controllers;

[Authorize]
public class LecturesController : Controller
{
    private readonly SchoolManagementDbContext _context;

    public LecturesController(SchoolManagementDbContext context)
    {
        _context = context;
    }

    // GET: Lectures
    public async Task<IActionResult> Index()
    {
          return _context.Lectures != null ? 
                      View(await _context.Lectures.ToListAsync()) :
                      Problem("Entity set 'SchoolManagementDbContext.Lectures'  is null.");
    }

    // GET: Lectures/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Lectures == null)
        {
            return NotFound();
        }

        var lecture = await _context.Lectures
            .FirstOrDefaultAsync(m => m.Id == id);
        if (lecture == null)
        {
            return NotFound();
        }

        return View(lecture);
    }

    // GET: Lectures/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Lectures/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,FirstName,LastName")] Lecture lecture)
    {
        if (ModelState.IsValid)
        {
            _context.Add(lecture);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(lecture);
    }

    // GET: Lectures/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Lectures == null)
        {
            return NotFound();
        }

        var lecture = await _context.Lectures.FindAsync(id);
        if (lecture == null)
        {
            return NotFound();
        }
        return View(lecture);
    }

    // POST: Lectures/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName")] Lecture lecture)
    {
        if (id != lecture.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(lecture);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LectureExists(lecture.Id))
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
        return View(lecture);
    }

    // GET: Lectures/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Lectures == null)
        {
            return NotFound();
        }

        var lecture = await _context.Lectures
            .FirstOrDefaultAsync(m => m.Id == id);
        if (lecture == null)
        {
            return NotFound();
        }

        return View(lecture);
    }

    // POST: Lectures/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Lectures == null)
        {
            return Problem("Entity set 'SchoolManagementDbContext.Lectures'  is null.");
        }
        var lecture = await _context.Lectures.FindAsync(id);
        if (lecture != null)
        {
            _context.Lectures.Remove(lecture);
        }
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool LectureExists(int id)
    {
      return (_context.Lectures?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
