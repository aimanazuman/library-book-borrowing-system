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
        public async Task<ActionResult<IEnumerable<BorrowRecord>>> GetBorrowRecords()
        {
            return await _context.BorrowRecords.Include(br => br.Book).ToListAsync();
        }

        // GET: api/BorrowRecords/[numbers such as 4]
        [HttpGet("{id}")]
        public async Task<ActionResult<BorrowRecord>> GetBorrowRecord(int id)
        {
            var record = await _context.BorrowRecords.Include(br => br.Book).FirstOrDefaultAsync(br => br.RecordId == id);

            if (record == null)
            {
                return NotFound();
            }

            return record;
        }

        // POST: api/BorrowRecords (Borrow Book)
        [HttpPost]
        public async Task<ActionResult<BorrowRecord>> PostBorrowRecord(BorrowRecord record)
        {
            var book = await _context.Books.FindAsync(record.BookId);
            if (book == null)
            {
                return NotFound("Book not found");
            }

            if (!book.IsAvailable)
            {
                return BadRequest("Book is not available");
            }

            // Set book as unavailable
            book.IsAvailable = false;
            record.Status = "Borrowed";
            record.BorrowDate = DateTime.Now;
            record.DueDate = DateTime.Now.AddDays(14); // 14 days borrow period

            _context.BorrowRecords.Add(record);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBorrowRecord), new { id = record.RecordId }, record);
        }

        // PUT: api/BorrowRecords/Return/[numbers such as 4] (Return Book)
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

            // Update record
            record.ReturnDate = DateTime.Now;
            record.Status = "Returned";

            // Set book as available
            record.Book.IsAvailable = true;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/BorrowRecords/[numbers such as 4]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrowRecord(int id)
        {
            var record = await _context.BorrowRecords.FindAsync(id);
            if (record == null)
            {
                return NotFound();
            }

            _context.BorrowRecords.Remove(record);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
