using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PatientsAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "City", "DateOfBirth", "FirstName", "Gender", "LastName", "Phone" },
                values: new object[,]
                {
                    { 1, "Villeurbanne", new DateTime(1966, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sophie", 1, "Martin", "04 78 01 23 45" },
                    { 2, "Lyon", new DateTime(1945, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jean", 0, "Durand", "04 72 34 56 78" },
                    { 3, "Lyon", new DateTime(2004, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lucas", 0, "Moreau", "04 78 56 78 90" },
                    { 4, "Vénissieux", new DateTime(2002, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emma", 1, "Lefevre", "04 71 23 45 67" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
