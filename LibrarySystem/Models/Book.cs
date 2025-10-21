// Models/Book.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Collections.Specialized.BitVector32;

namespace LibrarySystem.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        [Required]
        [StringLength(50)]
        public string ISBN { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        public int SectionId { get; set; }

        [StringLength(20)]
        public string RackId { get; set; }

        public bool IsAvailable { get; set; } = true;

        public DateTime DateAdded { get; set; } = DateTime.Now;

        // Navigation
        [ForeignKey("SectionId")]
        public Section Section { get; set; }

        public ICollection<BorrowRecord> BorrowRecords { get; set; }
    }
}
