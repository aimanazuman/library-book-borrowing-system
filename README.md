# Library Management System

A comprehensive ASP.NET Core MVC web application with RESTful API for efficient library operations including book management, borrowing system, and section organization with dynamic rack assignments.

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-blue)
![C#](https://img.shields.io/badge/C%23-12.0-green)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-red)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-purple)

## Features

- **📖 Book Management** - Add, update, view, and delete books with detailed information
- **👥 Borrowing System** - Track book loans with automatic due date calculation (14 days)
- **📍 Section Organization** - Organize books by sections with customizable rack IDs
- **🔍 Dynamic Rack Assignment** - Configure unique rack IDs for each library section
- **📊 Comprehensive Reports** - View all books and borrowing records with real-time status
- **✅ Smart Validation** - Prevent deletion of sections with books and books with active loans
- **🔄 Return Management** - Easy book return process with automatic availability updates
- **📱 Responsive Design** - Clean, professional interface optimized for all devices
- **🛡️ Data Integrity** - Foreign key relationships and validation ensure data consistency
- **⚡ RESTful API** - Complete CRUD operations accessible via API endpoints

## Technologies Used

### Backend
- **ASP.NET Core MVC 8.0** - Web framework
- **C# 12.0** - Programming language
- **Entity Framework Core** - ORM for database operations
- **SQL Server** - Relational database
- **RESTful API** - API architecture with DTO pattern
- **LINQ** - Data querying
- **Dependency Injection** - Service management

### Frontend
- **Razor Views** - Server-side rendering
- **Vanilla JavaScript** - Client-side functionality
- **CSS3** - Modern styling with flexbox and grid
- **Fetch API** - Asynchronous HTTP requests
- **Responsive Design** - Mobile-first approach

### Database
- **SQL Server Management Studio (SSMS)** - Database management
- **Code-First Migrations** - Database schema management
- **Seed Data** - Initial data population

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (recommended) or [Visual Studio Code](https://code.visualstudio.com/)
- [SQL Server 2022](https://www.microsoft.com/sql-server/sql-server-downloads) or SQL Server Express
- [SQL Server Management Studio (SSMS)](https://aka.ms/ssmsfullsetup) - Optional but recommended

## Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/aimanazuman/LibrarySystem.git
cd LibrarySystem
```

### 2. Configure Database Connection
Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=LibraryDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**Note:** Replace `localhost` with your SQL Server instance name if different.

### 3. Restore Dependencies
```bash
dotnet restore
```

### 4. Create Database
Open Package Manager Console in Visual Studio and run:
```bash
Add-Migration InitialCreate
Update-Database
```

This will create the database with three pre-configured sections and sample books.

### 5. Update API URL
In `wwwroot/js/site.js`, update the API URL with your port:
```javascript
window.API_URL = 'http://localhost:5287/api'; // Change port to match your application
```

### 6. Run the Application
```bash
dotnet run
```

Or press `F5` in Visual Studio.

### 7. Open in Browser
Navigate to `http://localhost:5287` (or the port shown in your console)

## Project Structure

```
LibrarySystem/
├── Controllers/
│   ├── HomeController.cs              # MVC page routing
│   ├── BooksController.cs             # Books API endpoints
│   ├── SectionsController.cs          # Sections API endpoints
│   └── BorrowRecordsController.cs     # Borrowing API endpoints
├── Models/
│   ├── Book.cs                        # Book entity model
│   ├── Section.cs                     # Section entity model
│   └── BorrowRecord.cs                # Borrow record entity model
├── Data/
│   └── LibraryContext.cs              # Database context with seed data
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml               # Homepage with features
│   │   ├── AddBook.cshtml             # Add book form
│   │   ├── BorrowBook.cshtml          # Borrow book form
│   │   ├── ViewReport.cshtml          # Books and records report
│   │   ├── ManageSections.cshtml      # Section management with racks
│   │   ├── About.cshtml               # Team information
│   │   └── FAQ.cshtml                 # Frequently asked questions
│   ├── Shared/
│   │   ├── _Layout.cshtml             # Main layout with navigation
│   │   ├── _ViewStart.cshtml          # View configuration
│   │   └── _ViewImports.cshtml        # Shared imports
├── wwwroot/
│   ├── css/
│   │   └── site.css                   # Application styles
│   └── js/
│       └── site.js                    # Client-side logic
├── Program.cs                         # Application configuration
└── appsettings.json                   # Configuration settings
```

## Usage Guide

### Managing Books
1. **Add Book**: Navigate to "Add Book" page
   - Fill in title, author, ISBN, category
   - Select section (description will appear)
   - Choose rack ID from dropdown
   - Submit to add book

2. **View Books**: Go to "View Report" page
   - See all books with availability status
   - Delete available books (borrowed books protected)

### Borrowing System
1. **Borrow Book**: Navigate to "Borrow Book" page
   - Select available book from dropdown
   - Enter borrower name and email
   - Submit to borrow (14-day loan period)

2. **Return Book**: Go to "View Report" page
   - Find borrowed record
   - Click "Return" button
   - Book becomes available again

3. **Delete Records**: In "View Report" page
   - Click "Delete" on any borrow record
   - If record is "Borrowed", book becomes available

### Section Management
1. **Add Section**: Navigate to "Manage Sections" page
   - Enter section name (e.g., "History")
   - Add description
   - Configure rack IDs (comma-separated: H1, H2, H3)
   - Submit to create

2. **Edit Section**: In sections table
   - Click "Edit" button
   - Modify name, description, or rack IDs
   - Update to save changes

3. **Delete Section**: 
   - Sections with books cannot be deleted
   - Empty sections can be removed

## API Endpoints

### Books API
- `GET /api/books` - Get all books
- `GET /api/books/{id}` - Get specific book
- `POST /api/books` - Add new book
- `PUT /api/books/{id}` - Update book
- `DELETE /api/books/{id}` - Delete book

### Sections API
- `GET /api/sections` - Get all sections
- `GET /api/sections/{id}` - Get specific section
- `POST /api/sections` - Add new section
- `PUT /api/sections/{id}` - Update section
- `DELETE /api/sections/{id}` - Delete section

### Borrow Records API
- `GET /api/borrowrecords` - Get all records
- `GET /api/borrowrecords/{id}` - Get specific record
- `POST /api/borrowrecords` - Borrow book
- `PUT /api/borrowrecords/return/{id}` - Return book
- `DELETE /api/borrowrecords/{id}` - Delete record

## Data Validation & Business Rules

### Books
- Cannot delete books with existing borrow records
- Title, Author, ISBN are required fields
- Book availability automatically updated on borrow/return

### Sections
- Cannot delete sections containing books
- Section name and description required
- Rack IDs stored in localStorage for persistence

### Borrowing
- Only available books appear in borrow dropdown
- Due date automatically set to 14 days from borrow date
- Books become unavailable when borrowed
- Return date recorded on book return

## Key Features Explained

### Dynamic Rack Assignment
Each section can have custom rack IDs:
- Fiction: A1-A5, B1-B5
- Non-Fiction: C1-C5, D1-D5
- Reference: E1-E5, F1-F5
- Custom sections: User-defined (e.g., History: H1-H10)

### Smart Validation
- Books with active loans cannot be deleted
- Sections with books cannot be deleted
- Borrowed books cannot be borrowed again
- Returned books cannot be returned again

### Status Tracking
- **Books**: Available (green) / Borrowed (yellow)
- **Records**: Borrowed (yellow) / Returned (blue)

## Testing Guide

### Using Postman
Test API endpoints directly:

**Add Book Example:**
```json
POST http://localhost:5287/api/books
Content-Type: application/json

{
  "title": "Clean Code",
  "author": "Robert Martin",
  "isbn": "978-0132350884",
  "category": "Programming",
  "sectionId": 2,
  "rackId": "C1"
}
```

**Borrow Book Example:**
```json
POST http://localhost:5287/api/borrowrecords
Content-Type: application/json

{
  "bookId": 1,
  "borrowerName": "John Doe",
  "borrowerEmail": "john@example.com"
}
```

### Testing Checklist
- [ ] Add book with all sections
- [ ] Borrow available book
- [ ] Return borrowed book
- [ ] Delete returned record
- [ ] Try deleting borrowed book (should fail)
- [ ] Add new section with custom racks
- [ ] Add book to new section
- [ ] View reports showing all data

## Database Schema

### Books Table
- BookId (PK)
- Title
- Author
- ISBN
- Category
- SectionId (FK)
- RackId
- IsAvailable
- DateAdded

### Sections Table
- SectionId (PK)
- SectionName
- Description

### BorrowRecords Table
- RecordId (PK)
- BookId (FK)
- BorrowerName
- BorrowerEmail
- BorrowDate
- DueDate
- ReturnDate
- Status

## Academic Context

**Course**: Web API Development  
**Course Code**: SWC3633/SWC4443  
**Objective**: CLO3 - Construct RESTful applications using appropriate methodologies  
**Project Type**: Group Project (Maximum 4 members)

### Assignment Requirements Fulfilled
- ✅ RESTful API with CRUD operations (GET, POST, PUT, DELETE)
- ✅ Home, About Us, and FAQ pages
- ✅ Two form pages (Add Book, Borrow Book)
- ✅ View report page with data tables
- ✅ Minimum two database tables (Books, Sections, BorrowRecords)
- ✅ Individual frontend implementations
- ✅ Collective backend development
- ✅ GitHub repository with documentation
- ✅ BPMN diagrams and API documentation

## Troubleshooting

### Common Issues

**Issue: API returns 400 Bad Request**
- Solution: Check that you're sending correct JSON format with required fields

**Issue: Database not found**
- Solution: Run `Update-Database` in Package Manager Console

**Issue: Books/Records not loading**
- Solution: Verify API URL in `site.js` matches your application port

**Issue: Cannot add book to new section**
- Solution: Configure rack IDs in Manage Sections before adding books

**Issue: CORS errors in browser console**
- Solution: Ensure `UseCors("AllowAll")` is correctly placed in Program.cs

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is open source and available for educational purposes.

## Acknowledgments

- **Microsoft** - For ASP.NET Core framework and Entity Framework Core
- **SQL Server** - For robust database management
- **Course Instructors** - For guidance and project requirements
- **Open Source Community** - For tools and libraries

## Support

If you encounter any issues:

1. Check the FAQ page in the application
2. Review the troubleshooting section above
3. Verify all prerequisites are installed
4. Check Visual Studio Output window for errors
5. Open an issue on GitHub with detailed error information

---

**If you found this project helpful, please give it a star!**

---

*This project was developed as part of the Web API Development course (SWC3633/SWC4443) to demonstrate RESTful API construction using ASP.NET Core MVC with Entity Framework Core.*