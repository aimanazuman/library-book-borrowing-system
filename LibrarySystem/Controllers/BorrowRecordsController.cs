using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Data;
using LibrarySystem.Models;

namespace LibrarySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowRecordsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BorrowRecordsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/BorrowRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetBorrowRecords()
        {
            var records = await _context.BorrowRecords
                .Include(br => br.Book)
                .Select(br => new
                {
                    br.RecordId,
                    br.BookId,
                    br.BorrowerName,
                    br.BorrowerEmail,
                    br.BorrowDate,
                    br.DueDate,
                    br.ReturnDate,
                    br.Status,
                    Book = new
                    {
                        br.Book.BookId,
                        br.Book.Title,
                        br.Book.Author,
                        br.Book.ISBN
                    }
                })
                .ToListAsync();

            return Ok(records);
        }

        // GET: api/BorrowRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetBorrowRecord(int id)
        {
            var record = await _context.BorrowRecords
                .Include(br => br.Book)
                .Where(br => br.RecordId == id)
                .Select(br => new
                {
                    br.RecordId,
                    br.BookId,
                    br.BorrowerName,
                    br.BorrowerEmail,
                    br.BorrowDate,
                    br.DueDate,
                    br.ReturnDate,
                    br.Status,
                    Book = new
                    {
                        br.Book.BookId,
                        br.Book.Title,
                        br.Book.Author,
                        br.Book.ISBN
                    }
                })
                .FirstOrDefaultAsync();

            if (record == null)
            {
                return NotFound();
            }

            return Ok(record);
        }

        // POST: api/BorrowRecords (Borrow Book)
        [HttpPost]
        public async Task<ActionResult> PostBorrowRecord([FromBody] BorrowRecordDto recordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = await _context.Books.FindAsync(recordDto.BookId);
            if (book == null)
            {
                return NotFound("Book not found");
            }

            if (!book.IsAvailable)
            {
                return BadRequest("Book is not available");
            }

            var record = new BorrowRecord
            {
                BookId = recordDto.BookId,
                BorrowerName = recordDto.BorrowerName,
                BorrowerEmail = recordDto.BorrowerEmail,
                BorrowDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14),
                Status = "Borrowed"
            };

            book.IsAvailable = false;

            _context.BorrowRecords.Add(record);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBorrowRecord), new { id = record.RecordId }, record);
        }

        // PUT: api/BorrowRecords/Return/5 (Return Book)
        [HttpPut("Return/{id}")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var record = await _context.BorrowRecords.Include(br => br.Book).FirstOrDefaultAsync(br => br.RecordId == id);

            if (record == null)
            {
                return NotFound();
            }

            if (record.Status == "Returned")
            {
                return BadRequest("Book already returned");
            }

            record.ReturnDate = DateTime.Now;
            record.Status = "Returned";
            record.Book.IsAvailable = true;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/BorrowRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrowRecord(int id)
        {
            var record = await _context.BorrowRecords.Include(br => br.Book).FirstOrDefaultAsync(br => br.RecordId == id);
            if (record == null)
            {
                return NotFound();
            }

            if (record.Status == "Borrowed" && record.Book != null)
            {
                record.Book.IsAvailable = true;
            }

            _context.BorrowRecords.Remove(record);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    // DTO for BorrowRecord - no navigation properties
    public class BorrowRecordDto
    {
        public int BookId { get; set; }
        public string BorrowerName { get; set; }
        public string BorrowerEmail { get; set; }
    }
}