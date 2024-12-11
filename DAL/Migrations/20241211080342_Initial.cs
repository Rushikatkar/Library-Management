using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    IsBorrowed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BorrowingHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BorrowedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LateFee = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowingHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BorrowingHistories_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowingHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Biography", "Name" },
                values: new object[,]
                {
                    { 1, "British author, best known for writing the Harry Potter series.", "J.K. Rowling" },
                    { 2, "English mathematician, physicist, and astronomer, widely recognized for his laws of motion and gravity.", "Isaac Newton" },
                    { 3, "English novelist, essayist, journalist, and critic, famous for works like '1984' and 'Animal Farm'.", "George Orwell" },
                    { 4, "Theoretical physicist best known for developing the theory of relativity and the famous equation E=mc^2.", "Albert Einstein" },
                    { 5, "Renaissance polymath known for his contributions to art, science, and engineering, including masterpieces like 'Mona Lisa'.", "Leonardo da Vinci" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fiction" },
                    { 2, "Science" },
                    { 3, "History" },
                    { 4, "Technology" },
                    { 5, "Art" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "LastLogin", "PasswordHash", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 11, 8, 3, 41, 742, DateTimeKind.Utc).AddTicks(1612), "rohan@gmail.com", true, null, "$2a$12$TcF6EKxfOoayF7Q6yNlP/.0KkvV5xGgfAZ3XOU1GG.0XjK1J45o1a", "User", "Rohan" },
                    { 2, new DateTime(2024, 12, 11, 8, 3, 41, 742, DateTimeKind.Utc).AddTicks(1616), "rajkumar@gmail.com", true, null, "$2b$12$aKsTfYEsr9OzSJi4/SUWCuUNhYGQcD0GDPwaTgJWRaqOgWm9IZubK", "Admin", "Raj" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CategoryId", "IsBorrowed", "Price", "PublishedDate", "Stock", "Title" },
                values: new object[,]
                {
                    { 1, 1, 1, false, 39.990000000000002, new DateTime(1997, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Harry Potter and the Philosopher's Stone" },
                    { 2, 2, 2, false, 49.990000000000002, new DateTime(1687, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Principia Mathematica" },
                    { 3, 3, 1, false, 29.989999999999998, new DateTime(1949, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "1984" },
                    { 4, 4, 2, false, 34.990000000000002, new DateTime(1916, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Relativity: The Special and General Theory" },
                    { 5, 5, 5, false, 59.990000000000002, new DateTime(1952, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "The Notebooks of Leonardo da Vinci" }
                });

            migrationBuilder.InsertData(
                table: "BorrowingHistories",
                columns: new[] { "Id", "BookId", "BorrowedDate", "LateFee", "ReturnedDate", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 12, 1, 8, 3, 41, 742, DateTimeKind.Utc).AddTicks(2065), 0.0, new DateTime(2024, 12, 9, 8, 3, 41, 742, DateTimeKind.Utc).AddTicks(2070), 1 },
                    { 2, 2, new DateTime(2024, 11, 26, 8, 3, 41, 742, DateTimeKind.Utc).AddTicks(2076), 0.0, null, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowingHistories_BookId",
                table: "BorrowingHistories",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowingHistories_UserId",
                table: "BorrowingHistories",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowingHistories");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
