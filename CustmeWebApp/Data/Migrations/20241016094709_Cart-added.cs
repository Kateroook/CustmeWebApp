using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustmeWebApp.Data.Migrations
{
    public partial class Cartadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            // Створюємо таблицю Cart заново
            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false), // Зміна типу Id на string
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id); // Визначення первинного ключа
                });

            // Створюємо таблицю CartItems заново
            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"), // Властивість IDENTITY для стовпця Id
                    CartId = table.Column<string>(type: "nvarchar(450)", nullable: false), // Відповідний тип для CartId
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: true) // Зв'язок із таблицею Project
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id); // Визначення первинного ключа
                   
                    table.ForeignKey(
                        name: "FK_CartItems_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict); // Зв'язок із таблицею Project
                });

            // Індекси для оптимізації
            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProjectId",
                table: "CartItems",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Відновлюємо попередні налаштування (при необхідності)
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Cart");
        }
    }
}
