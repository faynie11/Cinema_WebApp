using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaBooking.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DurationMinutes = table.Column<int>(type: "integer", nullable: false),
                    PosterUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Row = table.Column<int>(type: "integer", nullable: false),
                    Column = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Screenings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MovieId = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screenings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Screenings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ScreeningId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Screenings_ScreeningId",
                        column: x => x.ScreeningId,
                        principalTable: "Screenings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationSeats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReservationId = table.Column<int>(type: "integer", nullable: false),
                    SeatId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationSeats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationSeats_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationSeats_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "Column", "Row" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 1 },
                    { 4, 4, 1 },
                    { 5, 5, 1 },
                    { 6, 6, 1 },
                    { 7, 7, 1 },
                    { 8, 8, 1 },
                    { 9, 9, 1 },
                    { 10, 10, 1 },
                    { 11, 1, 2 },
                    { 12, 2, 2 },
                    { 13, 3, 2 },
                    { 14, 4, 2 },
                    { 15, 5, 2 },
                    { 16, 6, 2 },
                    { 17, 7, 2 },
                    { 18, 8, 2 },
                    { 19, 9, 2 },
                    { 20, 10, 2 },
                    { 21, 1, 3 },
                    { 22, 2, 3 },
                    { 23, 3, 3 },
                    { 24, 4, 3 },
                    { 25, 5, 3 },
                    { 26, 6, 3 },
                    { 27, 7, 3 },
                    { 28, 8, 3 },
                    { 29, 9, 3 },
                    { 30, 10, 3 },
                    { 31, 1, 4 },
                    { 32, 2, 4 },
                    { 33, 3, 4 },
                    { 34, 4, 4 },
                    { 35, 5, 4 },
                    { 36, 6, 4 },
                    { 37, 7, 4 },
                    { 38, 8, 4 },
                    { 39, 9, 4 },
                    { 40, 10, 4 },
                    { 41, 1, 5 },
                    { 42, 2, 5 },
                    { 43, 3, 5 },
                    { 44, 4, 5 },
                    { 45, 5, 5 },
                    { 46, 6, 5 },
                    { 47, 7, 5 },
                    { 48, 8, 5 },
                    { 49, 9, 5 },
                    { 50, 10, 5 },
                    { 51, 1, 6 },
                    { 52, 2, 6 },
                    { 53, 3, 6 },
                    { 54, 4, 6 },
                    { 55, 5, 6 },
                    { 56, 6, 6 },
                    { 57, 7, 6 },
                    { 58, 8, 6 },
                    { 59, 9, 6 },
                    { 60, 10, 6 },
                    { 61, 1, 7 },
                    { 62, 2, 7 },
                    { 63, 3, 7 },
                    { 64, 4, 7 },
                    { 65, 5, 7 },
                    { 66, 6, 7 },
                    { 67, 7, 7 },
                    { 68, 8, 7 },
                    { 69, 9, 7 },
                    { 70, 10, 7 },
                    { 71, 1, 8 },
                    { 72, 2, 8 },
                    { 73, 3, 8 },
                    { 74, 4, 8 },
                    { 75, 5, 8 },
                    { 76, 6, 8 },
                    { 77, 7, 8 },
                    { 78, 8, 8 },
                    { 79, 9, 8 },
                    { 80, 10, 8 },
                    { 81, 1, 9 },
                    { 82, 2, 9 },
                    { 83, 3, 9 },
                    { 84, 4, 9 },
                    { 85, 5, 9 },
                    { 86, 6, 9 },
                    { 87, 7, 9 },
                    { 88, 8, 9 },
                    { 89, 9, 9 },
                    { 90, 10, 9 },
                    { 91, 1, 10 },
                    { 92, 2, 10 },
                    { 93, 3, 10 },
                    { 94, 4, 10 },
                    { 95, 5, 10 },
                    { 96, 6, 10 },
                    { 97, 7, 10 },
                    { 98, 8, 10 },
                    { 99, 9, 10 },
                    { 100, 10, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ScreeningId",
                table: "Reservations",
                column: "ScreeningId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationSeats_ReservationId",
                table: "ReservationSeats",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationSeats_SeatId",
                table: "ReservationSeats",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Screenings_MovieId",
                table: "Screenings",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationSeats");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Screenings");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
