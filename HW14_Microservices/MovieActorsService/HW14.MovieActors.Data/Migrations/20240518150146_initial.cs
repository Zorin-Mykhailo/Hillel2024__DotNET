using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HW14.MovieActors.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "DateOfBirth", "Description", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(1970, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Актор, сценарист, режисер, продюсер", "Ітан", "Гоук" },
                    { 2, new DateTime(1969, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Актриса, продюсер, режисер", "Дженніфер", "Еністон" },
                    { 3, new DateTime(1948, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Актор", "Жан", "Рено" },
                    { 4, new DateTime(1938, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Актор", "Крістофер", "Ллойд" },
                    { 5, new DateTime(1961, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Актор", "Майкл", "Дж. Фокс" },
                    { 6, new DateTime(1979, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Актриса", "Дженніфер", "Моррісон" },
                    { 7, new DateTime(1976, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Актриса", "Дайан", "Крюгер" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actors");
        }
    }
}
