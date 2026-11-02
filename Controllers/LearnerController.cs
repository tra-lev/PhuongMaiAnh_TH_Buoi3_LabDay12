
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pma_Lab05.Models;

public class LearnerController : Controller
{
    private readonly SchoolContext _context;

    public LearnerController(SchoolContext context)
    {
        _context = context;
    }

    // GET: Learners
    public async Task<IActionResult> Index()
    {
        var learners = _context.Learners.Include(l => l.Major);
        return View(await learners.ToListAsync());
    }

    // GET: Learners/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var learner = await _context.Learners
            .Include(l => l.Major)
            .FirstOrDefaultAsync(m => m.LearnerId == id);

        if (learner == null)
        {
            return NotFound();
        }

        return View(learner);
    }

    // GET: Learners/Create
    public IActionResult Create()
    {
        ViewData["MajorId"] = new SelectList(
            _context.Majors, "MajorId", "MajorName");

        return View();
    }

    // POST: Learners/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("LastName,FirstMidName,EnrollmentDate,MajorId")] Learner learner)
    {
        if (ModelState.IsValid)
        {
            _context.Add(learner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["MajorId"] = new SelectList(
            _context.Majors, "MajorId", "MajorName", learner.MajorId);

        return View(learner);
    }

    // GET: Learners/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var learner = await _context.Learners.FindAsync(id);

        if (learner == null)
        {
            return NotFound();
        }

        ViewData["MajorId"] = new SelectList(
            _context.Majors, "MajorId", "MajorName", learner.MajorId);

        return View(learner);
    }

    // POST: Learners/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("LearnerId,LastName,FirstMidName,EnrollmentDate,MajorId")] Learner learner)
    {
        if (id != learner.LearnerId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(learner);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LearnerExists(learner.LearnerId))
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

        ViewData["MajorId"] = new SelectList(
            _context.Majors, "MajorId", "MajorName", learner.MajorId);

        return View(learner);
    }

    // GET: Learners/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var learner = await _context.Learners
            .Include(l => l.Major)
            .FirstOrDefaultAsync(m => m.LearnerId == id);

        if (learner == null)
        {
            return NotFound();
        }

        return View(learner);
    }

    // POST: Learners/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var learner = await _context.Learners.FindAsync(id);

        if (learner != null)
        {
            _context.Learners.Remove(learner);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool LearnerExists(int id)
    {
        return _context.Learners.Any(e => e.LearnerId == id);
    }
}
