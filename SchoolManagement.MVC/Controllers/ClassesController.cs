using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.MVC.Data;
using SchoolManagement.MVC.Models;

namespace SchoolManagement.MVC.Controllers;

[Authorize]
public class ClassesController : Controller
{
    private readonly SchoolManagementDbContext _context;
    private readonly INotyfService _notyfService;

    public ClassesController(SchoolManagementDbContext context, INotyfService notyfService)
    {
        _context = context;
        _notyfService = notyfService;
    }

    // GET: Classes
    public async Task<IActionResult> Index()
    {
        var schoolManagementDbContext = _context.Classes
            .Include(context => context.Course)
            .Include(context => context.Lecture);
        return View(await schoolManagementDbContext.ToListAsync());
    }

    // GET: Classes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Classes == null)
        {
            return NotFound();
        }

        var @class = await _context.Classes
            .Include(q => q.Course)
            .Include(q => q.Lecture)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (@class == null)
        {
            return NotFound();
        }

        return View(@class);
    }

    // GET: Classes/Create
    public IActionResult Create()
    {
        //CreateSelectList();

        ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
        ViewData["LectureId"] = new SelectList(_context.Lectures, "Id", "FirstName");
        return View();
    }

    // POST: Classes/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,LectureId,CourseId,Time")] Classes @class)
    {
        if (ModelState.IsValid)
        {
            _context.Add(@class);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", @class.CourseId);
        ViewData["LectureId"] = new SelectList(_context.Lectures, "Id", "Id", @class.LectureId);

        return View(@class);
    }

    // GET: Classes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Classes == null)
        {
            return NotFound();
        }

        var @class = await _context.Classes.FindAsync(id);
        if (@class == null)
        {
            return NotFound();
        }

        ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", @class.CourseId);
        ViewData["LectureId"] = new SelectList(_context.Lectures, "Id", "Id", @class.LectureId);

        return View(@class);
    }

    // POST: Classes/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,LectureId,CourseId,Time")] Classes @class)
    {
        if (id != @class.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(@class);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(@class.Id))
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
        ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", @class.CourseId);
        ViewData["LectureId"] = new SelectList(_context.Lectures, "Id", "Id", @class.LectureId);

        return View(@class);
    }

    // GET: Classes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Classes == null)
        {
            return NotFound();
        }

        var @class = await _context.Classes
            .Include(q => q.Course)
            .Include(q => q.Lecture)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (@class == null)
        {
            return NotFound();
        }

        return View(@class);
    }

    // POST: Classes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Classes == null)
        {
            return Problem("Entity set 'SchoolManagementDbContext.Classes'  is null.");
        }
        var @class = await _context.Classes.FindAsync(id);
        if (@class != null)
        {
            _context.Classes.Remove(@class);
        }
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<ActionResult> ManageEnrollments(int classId)
    {
        var @class = await _context.Classes
            .Include(q => q.Course)
            .Include(q => q.Lecture)
            .Include(q => q.Enrollments)
                .ThenInclude(q => q.Student)
            .FirstOrDefaultAsync(enroll => enroll.Id == classId);

        var students = await _context.Students.ToListAsync();

        var model = new ClassEnrollmentViewModel
        {
            Classes = new ClassViewModel
            {
                Id = @class.Id,
                CourseName = $"{@class.Course?.Code} - {@class.Course?.Name}",
                LectureName = $"{@class.Lecture?.FirstName} {@class.Lecture?.LastName}",
                Time = @class.Time.ToString()
            }
        };

        foreach (var student in students)
        {
            model.StudentEnrollment.Add(new StudentEnrollmentViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                IsEnrolled = (@class?.Enrollments.Any(q => q.StudentId == student.Id))
                .GetValueOrDefault()
            }); 
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> EnrollStudent(int classId, int studentId, bool shouldEnroll)
    {
        var enrollment = new Enrollment();
        if(shouldEnroll == true)
        {
            enrollment.ClassId = classId;
            enrollment.StudentId = studentId;
            await _context.AddAsync(enrollment);
            _notyfService.Success("Student Enrolled Successfully");
        }
        else
        {
            enrollment = await _context.Enrollments.FirstOrDefaultAsync(q => q.ClassId == classId && q.StudentId == studentId);

            if(enrollment != null)
            {
                _context.Remove(enrollment);
                _notyfService.Warning("Student Unenrolled Successfully");
            }
        }

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(ManageEnrollments), new { classId });
    }

    private bool ClassExists(int id)
    {
      return (_context.Classes?.Any(e => e.Id == id)).GetValueOrDefault();
    }

    /* REFACTOR OF CREATE CLASS DB QUERY
     -----------------------------------------------------------------------------*/
    /*private void CreateSelectList() *//* NOT WORKING NULLREFERENCE EXEPTION*//*
    {
        var courses = _context.Courses.Select(course => new
        {
            CourseName = $"{course.Code} - {course.Name} ({course.Credits} Credits)",
            course.Id
        });

        ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "CourseName");

        
        var lectures = _context.Lectures.Select(lecture => new
        {
            Fullname = $"{lecture.FirstName} {lecture.LastName}",
            lecture.Id
        });

        ViewData["LectureId"] = new SelectList(_context.Lectures, "Id", "Fullname");
    }*/
}
