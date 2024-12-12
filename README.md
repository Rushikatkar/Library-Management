# Library Management System

## Overview
The **Library Management System** is a robust .NET Core Web API designed to streamline and manage library operations. This project incorporates extensibility, scalability, and maintainability principles, making it an ideal solution for libraries of any size. The API provides secure and efficient management of books, authors, categories, users, and borrowing histories.

## Features
- **Comprehensive CRUD Operations**: Manage books, authors, categories, users, and borrowing histories.
- **JWT-Based Authentication and Authorization**: Includes role-based access control for Admin and User roles.
- **Filtering, Sorting, and Pagination**: Enhanced querying for books.
- **Late Fee Calculation**: Automated late fee calculation for users returning books after the due date.
- **Global Exception Handling and Logging**: Ensures a seamless user experience with detailed error handling.
- **API Documentation**: Integrated Swagger UI for easy testing and exploration.

## Technology Stack
- **Framework**: .NET Core 8
- **Database**: SQL Server with Entity Framework Core (Code-First Approach)
- **Authentication**: JWT (JSON Web Token)
- **Caching**: In-memory caching
- **Testing**: xUnit and Moq

## Project Structure
```
LibraryManagement
├── Data Access Layer (DAL)
    ├── DTO
    ├── Models
    ├── Repositories
├── Business Access Layer (BAL)
    ├── Services
    ├── Mappings
├── WEB APPLICATION (WEB API)
    ├── Controllers
    ├── Middleware

```

## Prerequisites
- Visual Studio 2022 or later
- .NET 8 SDK
- SQL Server

## Setup Instructions
1. Clone the repository:  
   ```bash
   git clone https://github.com/Rushikatkar/Library-Management.git
   ```
2. Navigate to the project directory:  
   ```bash
   cd Library-Management
   ```
3. Configure the connection string in `appsettings.json`.
4. Apply migrations to set up the database schema:  
   ```bash
   dotnet ef database update
   ```
5. Run the application:  
   ```bash
   dotnet run
   ```
6. Access the Swagger UI at `http://localhost:{port}/swagger`.

---

## API Documentation

### Models

#### Book
```json
{
  "Id": "int",
  "Title": "string",
  "AuthorId": "int",
  "CategoryId": "int",
  "PublishedYear": "int",
  "ISBN": "string"
}
```
- **Endpoints**:
  - `GET /api/books` - Retrieves all books with optional filtering, sorting, and pagination.
  - `POST /api/books` - Creates a new book.
  - `PUT /api/books/{id}` - Updates an existing book.
  - `DELETE /api/books/{id}` - Deletes a book by ID.

#### Author
```json
{
  "Id": "int",
  "Name": "string",
  "Biography": "string"
}
```
- **Endpoints**:
  - `GET /api/authors` - Retrieves all authors.
  - `POST /api/authors` - Creates a new author.
  - `PUT /api/authors/{id}` - Updates an existing author.
  - `DELETE /api/authors/{id}` - Deletes an author by ID.

#### Category
```json
{
  "Id": "int",
  "Name": "string"
}
```
- **Endpoints**:
  - `GET /api/categories` - Retrieves all categories.
  - `POST /api/categories` - Creates a new category.
  - `PUT /api/categories/{id}` - Updates an existing category.
  - `DELETE /api/categories/{id}` - Deletes a category by ID.

#### User
```json
{
  "Id": "int",
  "Name": "string",
  "Email": "string",
  "Role": "string"
}
```
- **Endpoints**:
  - `GET /api/users` - Retrieves all users.
  - `POST /api/users` - Creates a new user.
  - `PUT /api/users/{id}` - Updates an existing user.
  - `DELETE /api/users/{id}` - Deletes a user by ID.

#### BorrowingHistory
```json
{
  "Id": "int",
  "UserId": "int",
  "BookId": "int",
  "BorrowedDate": "datetime",
  "ReturnDate": "datetime",
  "LateFee": "decimal"
}
```
- **Endpoints**:
  - `GET /api/borrowinghistories` - Retrieves all borrowing histories.
  - `POST /api/borrowinghistories` - Logs a new borrowing transaction.
  - `PUT /api/borrowinghistories/{id}` - Updates an existing borrowing history.
  - `DELETE /api/borrowinghistories/{id}` - Deletes a borrowing history by ID.

---

## License
This project is licensed under the MIT License.
