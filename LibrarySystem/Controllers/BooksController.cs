using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Data;
using LibrarySystem.Models;

namespace LibrarySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetBooks()
        {
            var books = await _context.Books
                .Include(b => b.Section)
                .Select(b => new
                {
                    b.BookId,
                    b.Title,
                    b.Author,
                    b.ISBN,
                    b.Category,
                    b.SectionId,
                    b.RackId,
                    b.IsAvailable,
                    b.DateAdded,
                    Section = new
                    {
                        b.Section.SectionId,
                        b.Section.SectionName,
                        b.Section.Description
                    }
                })
                .ToListAsync();

            return Ok(books);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetBook(int id)
        {
            var book = await _context.Books
                .Include(b => b.Section)
                .Where(b => b.BookId == id)
                .Select(b => new
                {
                    b.BookId,
                    b.Title,
                    b.Author,
                    b.ISBN,
                    b.Category,
                    b.SectionId,
                    b.RackId,
                    b.IsAvailable,
                    b.DateAdded,
                    Section = new
                    {
                        b.Section.SectionId,
                        b.Section.SectionName,
                        b.Section.Description
                    }
                })
                .FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult> PostBook([FromBody] BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                ISBN = bookDto.ISBN,
                Category = bookDto.Category,
                SectionId = bookDto.SectionId,
                RackId = bookDto.RackId,
                IsAvailable = true,
                DateAdded = DateTime.Now
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, book);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, [FromBody] BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            book.Title = bookDto.Title;
            book.Author = bookDto.Author;
            book.ISBN = bookDto.ISBN;
            book.Category = bookDto.Category;
            book.SectionId = bookDto.SectionId;
            book.RackId = bookDto.RackId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var hasBorrowRecords = await _context.BorrowRecords.AnyAsync(br => br.BookId == id);
            if (hasBorrowRecords)
            {
                return BadRequest("Cannot delete book with existing borrow records.");
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }

    // DTO for Book - no navigation properties
    public class BookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string? Category { get; set; }
        public int SectionId { get; set; }
        public string RackId { get; set; }
    }
}