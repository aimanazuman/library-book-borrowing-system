using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Data;
using LibrarySystem.Models;

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
        public async Task<ActionResult<IEnumerable<object>>> GetSections()
        {
            var sections = await _context.Sections
                .Select(s => new
                {
                    s.SectionId,
                    s.SectionName,
                    s.Description,
                    Books = s.Books.Select(b => new
                    {
                        b.BookId,
                        b.Title,
                        b.Author,
                        b.IsAvailable
                    }).ToList()
                })
                .ToListAsync();

            return Ok(sections);
        }

        // GET: api/Sections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetSection(int id)
        {
            var section = await _context.Sections
                .Where(s => s.SectionId == id)
                .Select(s => new
                {
                    s.SectionId,
                    s.SectionName,
                    s.Description,
                    Books = s.Books.Select(b => new
                    {
                        b.BookId,
                        b.Title,
                        b.Author,
                        b.IsAvailable
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (section == null)
            {
                return NotFound();
            }

            return Ok(section);
        }

        // POST: api/Sections
        [HttpPost]
        public async Task<ActionResult> PostSection([FromBody] SectionDto sectionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var section = new Section
            {
                SectionName = sectionDto.SectionName,
                Description = sectionDto.Description
            };

            _context.Sections.Add(section);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSection), new { id = section.SectionId }, section);
        }

        // PUT: api/Sections/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSection(int id, [FromBody] SectionDto sectionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }

            section.SectionName = sectionDto.SectionName;
            section.Description = sectionDto.Description;

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

        // DELETE: api/Sections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSection(int id)
        {
            var section = await _context.Sections.Include(s => s.Books).FirstOrDefaultAsync(s => s.SectionId == id);
            if (section == null)
            {
                return NotFound();
            }

            if (section.Books != null && section.Books.Any())
            {
                return BadRequest("Cannot delete section with existing books.");
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

    // DTO for Section - no navigation properties
    public class SectionDto
    {
        public string SectionName { get; set; }
        public string? Description { get; set; }
    }
}