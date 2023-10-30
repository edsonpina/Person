using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Person.API.Model;

namespace Person.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class PersonController : Controller
    {
        private readonly ILogger<PersonController> _logger;
        private readonly PersonContext _context;

        public PersonController(ILogger<PersonController> logger, PersonContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: People
        [HttpGet]
        public async Task<IActionResult> Index(bool jsonFormat = true)
        {
            if (jsonFormat)
                return Json(_context.People != null ?
                          View(await _context.People.OrderByDescending(o => o.Id).ToListAsync()) :
                          Problem("Entity set 'PersonContext.People'  is null."));

            return _context.People != null ?
                        View(await _context.People.ToListAsync()) :
                        Problem("Entity set 'PersonContext.People'  is null.");
        }

        // GET: People/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id, bool jsonFormat = true)
        {
            if (id == null || _context.People == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            if (jsonFormat)
                return Json(person);

            return View(person);
        }

        // GET: People/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        [HttpPost(Name = "PersonCreate")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BirthDate,Document,Employee,id,name")] Model.Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.People == null)
            {
                return NotFound();
            }

            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        [HttpPut]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BirthDate,Document,Employee,id,name")] Model.Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok();
            }
            return Ok();
        }

        // GET: People/Delete/5
        [HttpGet, ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.People == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpDelete, ActionName("DeleteConfirmed")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.People == null)
            {
                return Problem("Entity set 'PersonContext.People'  is null.");
            }
            if (!PersonExists(id))
            {
                return NotFound();
            }
            var person = await _context.People.FindAsync(id);
            if (person != null)
            {
                _context.People.Remove(person);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool PersonExists(int id)
        {
            return (_context.People?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
