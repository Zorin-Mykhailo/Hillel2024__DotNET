using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HW14.MovieManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    RoomName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2024, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Приборкувачка драконів" },
                    { 2, "", new DateTime(2024, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Повстання Штатів" },
                    { 3, "", new DateTime(2024, 5, 18, 11, 6, 40, 404, DateTimeKind.Local).AddTicks(4437), "Панда Кунг-Фу 4" }
                });

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "Id", "MovieId", "RoomName", "StartAt" },
                values: new object[,]
                {
                    { 1, 1, "Зал 4", new DateTime(2024, 4, 11, 14, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, "Зал 4", new DateTime(2024, 4, 11, 16, 40, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 2, "Зал 1", new DateTime(2024, 4, 11, 14, 50, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 2, "Зал 2", new DateTime(2024, 4, 11, 15, 50, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 2, "Зал 1", new DateTime(2024, 4, 11, 17, 10, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 2, "Зал 1", new DateTime(2024, 4, 11, 19, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 2, "Зал 2", new DateTime(2024, 4, 11, 20, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 3, "Зал 7", new DateTime(2024, 4, 11, 14, 20, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 3, "Зал 7", new DateTime(2024, 4, 11, 16, 20, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_MovieId",
                table: "Sessions",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
