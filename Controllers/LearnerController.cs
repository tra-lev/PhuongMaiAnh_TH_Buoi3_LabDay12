
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pma_Lab05.Models;

public class LearnerController : Controller
{
    private readonly SchoolContext _context;

    // Lab 6: khai báo biến toàn cục pageSize (số learner trên 1 trang)
    private int pageSize = 3;

    public LearnerController(SchoolContext context)
    {
        _context = context;
    }

    // GET: Learner/Index?mid=1
    // Lab 5 (1.2): nhận tham số mid để lọc theo chuyên ngành
    // Lab 6 (1):   phân trang, trả về trang đầu tiên
    public IActionResult Index(int? mid)
    {
        var learners = (IQueryable<Learner>)_context.Learners
            .Include(m => m.Major);

        if (mid != null)
        {
            learners = (IQueryable<Learner>)_context.Learners
                .Where(l => l.MajorId == mid)
                .Include(m => m.Major);
            ViewBag.mid = mid;
        }

        int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);

        ViewBag.pageNum = pageNum;
        ViewBag.pageIndex = 1;

        var result = learners.OrderBy(l => l.LearnerId)
                             .Take(pageSize)
                             .ToList();
        return View(result);
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

    public IActionResult LearnerByMajorID(int mid)
    {
        var learners = _context.Learners
            .Where(l => l.MajorId == mid)
            .Include(m => m.Major)
            .ToList();

        return PartialView("LearnerTable", learners);
    }

    public IActionResult LearnerFilter(int? mid, string? keyword, int? pageIndex)
    {
        var learners = (IQueryable<Learner>)_context.Learners;
        int page = (int)(pageIndex == null || pageIndex <= 0 ? 1 : pageIndex);

        if (mid != null)
        {
            // lọc
            learners = learners.Where(l => l.MajorId == mid);
            ViewBag.mid = mid;
        }

        if (!string.IsNullOrEmpty(keyword))
        {
            learners = learners.Where(l => l.FirstMidName.ToLower()
                                            .Contains(keyword.ToLower()));
            ViewBag.keyword = keyword;
        }

        int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);
        ViewBag.pageNum = pageNum;
        ViewBag.pageIndex = page;

        var result = learners.OrderBy(l => l.LearnerId)
                             .Skip(pageSize * (page - 1))
                             .Take(pageSize)
                             .Include(m => m.Major)
                             .ToList();
        return PartialView("LearnerTable", result);
    }
}
