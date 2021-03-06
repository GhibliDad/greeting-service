using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreetingService.Infrastructure.Migrations
{
    public partial class CreatedInvoiceTable3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "Greetings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    CostPerGreeting = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Users_SenderEmail",
                        column: x => x.SenderEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Greetings_InvoiceId",
                table: "Greetings",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_SenderEmail",
                table: "Invoice",
                column: "SenderEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Greetings_Invoice_InvoiceId",
                table: "Greetings",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Greetings_Invoice_InvoiceId",
                table: "Greetings");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Greetings_InvoiceId",
                table: "Greetings");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Greetings");
        }
    }
}
