// Controllers/SectionsController.cs
using LibrarySystem.Data;
using LibrarySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public SectionsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Sections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Section>>> GetSections()
        {
            return await _context.Sections.Include(s => s.Books).ToListAsync();
        }

        // GET: api/Sections/[numbers such as 2]
        [HttpGet("{id}")]
        public async Task<ActionResult<Section>> GetSection(int id)
        {
            var section = await _context.Sections.Include(s => s.Books).FirstOrDefaultAsync(s => s.SectionId == id);

            if (section == null)
            {
                return NotFound();
            }

            return section;
        }

        // POST: api/Sections
        [HttpPost]
        public async Task<ActionResult<Section>> PostSection(Section section)
        {
            _context.Sections.Add(section);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSection), new { id = section.SectionId }, section);
        }

        // PUT: api/Sections/[numbers such as 1]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSection(int id, Section section)
        {
            if (id != section.SectionId)
            {
                return BadRequest();
            }

            _context.Entry(section).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SectionExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Sections/[numbers such as 1]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSection(int id)
        {
            var section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }

            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SectionExists(int id)
        {
            return _context.Sections.Any(e => e.SectionId == id);
        }
    }
}