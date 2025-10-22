using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Models
{
    public class BorrowRecord
    {
        [Key]
        public int RecordId { get; set; }

        public int BookId { get; set; }

        [Required]
        [StringLength(100)]
        public string BorrowerName { get; set; }

        [Required]
        [StringLength(100)]
        public string BorrowerEmail { get; set; }

        public DateTime BorrowDate { get; set; } = DateTime.Now;

        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Borrowed"; // Borrowed, Returned, Overdue

        // Navigation
        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}