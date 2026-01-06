using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NLayer.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Plate = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DailyPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Mileage = table.Column<int>(type: "int", nullable: false),
                    isAvailable = table.Column<bool>(type: "bit", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    RentDate = table.Column<DateTime>(type: "date", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "date", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rentals_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreatedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ford", null },
                    { 2, new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Opel", null },
                    { 3, new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nissan", null },
                    { 4, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ferrari", null },
                    { 5, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Porsche", null }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedDate", "Email", "FirstName", "LastName", "Phone", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ayitkisakir@gmail.com", "Şakir", "Ayitki", "+90 555 555 55 55", null },
                    { 2, new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "eiayitki@gmail.com", "Elif", "Ayitki", "+90 444 444 44 44", null },
                    { 3, new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "ferhatcakmak@gmail.com", "Ferhat", "Cakmak", "+90 333 333 33 33", null },
                    { 4, new DateTime(2026, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "muhammet@gmail.com", "Muhammet", "Kara", "+90 777 777 77 77", null }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "BrandId", "CreatedDate", "DailyPrice", "Mileage", "Model", "Plate", "UpdatedDate", "Year", "isAvailable" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6500m, 120000, "Ranger", "34 AHC 16", null, 2025, true },
                    { 2, 2, new DateTime(2026, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2400m, 256000, "Astra", "34 TF 21", null, 2018, true },
                    { 3, 3, new DateTime(2026, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3200m, 310000, "Qashqai", "52 ORD 39", null, 2022, false },
                    { 4, 4, new DateTime(2025, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 65000m, 10000, "La Ferrari", "34 GEL 34", null, 2023, true },
                    { 5, 5, new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 45000m, 13540, "GT3 Turbo", "06 BNM 112", null, 2022, true }
                });

            migrationBuilder.InsertData(
                table: "Rentals",
                columns: new[] { "Id", "CarId", "CreatedDate", "CustomerId", "RentDate", "ReturnDate", "TotalPrice", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5500m, null },
                    { 2, 1, new DateTime(2025, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 10500m, null },
                    { 3, 2, new DateTime(2025, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 9500m, null },
                    { 4, 5, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 125000m, null },
                    { 5, 4, new DateTime(2026, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2026, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 55000m, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BrandId",
                table: "Cars",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_CarId",
                table: "Rentals",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_CustomerId",
                table: "Rentals",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
