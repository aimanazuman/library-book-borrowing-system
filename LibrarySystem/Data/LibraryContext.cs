using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Sections
            modelBuilder.Entity<Section>().HasData(
                new Section { SectionId = 1, SectionName = "Fiction", Description = "Fiction books collection" },
                new Section { SectionId = 2, SectionName = "Non-Fiction", Description = "Non-fiction books collection" },
                new Section { SectionId = 3, SectionName = "Reference", Description = "Reference materials" }
            );

            // Seed Books
            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", ISBN = "978-0743273565", Category = "Classic", SectionId = 1, RackId = "A1", IsAvailable = true, DateAdded = new DateTime(2025, 01, 01) },
                new Book { BookId = 2, Title = "1984", Author = "George Orwell", ISBN = "978-0451524935", Category = "Dystopian", SectionId = 1, RackId = "A2", IsAvailable = true, DateAdded = new DateTime(2025, 01, 01) },
                new Book { BookId = 3, Title = "Sapiens", Author = "Yuval Noah Harari", ISBN = "978-0062316097", Category = "History", SectionId = 2, RackId = "B1", IsAvailable = true, DateAdded = new DateTime(2025, 01, 01) }
            );
        }
    }
}