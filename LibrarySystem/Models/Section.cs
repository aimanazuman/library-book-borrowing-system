using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models
{
    public class Section
    {
        [Key]
        public int SectionId { get; set; }

        [Required]
        [StringLength(100)]
        public string SectionName { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}