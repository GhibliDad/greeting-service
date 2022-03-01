using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreetingService.Infrastructure.Migrations
{
    public partial class CreatedInvoiceTable5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Greetings_Invoice_InvoiceId",
                table: "Greetings");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Users_SenderEmail",
                table: "Invoice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice");

            migrationBuilder.RenameTable(
                name: "Invoice",
                newName: "Invoices");

            migrationBuilder.RenameIndex(
                name: "IX_Invoice_SenderEmail",
                table: "Invoices",
                newName: "IX_Invoices_SenderEmail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Greetings_Invoices_InvoiceId",
                table: "Greetings",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Users_SenderEmail",
                table: "Invoices",
                column: "SenderEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Greetings_Invoices_InvoiceId",
                table: "Greetings");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Users_SenderEmail",
                table: "Invoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices");

            migrationBuilder.RenameTable(
                name: "Invoices",
                newName: "Invoice");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_SenderEmail",
                table: "Invoice",
                newName: "IX_Invoice_SenderEmail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Greetings_Invoice_InvoiceId",
                table: "Greetings",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Users_SenderEmail",
                table: "Invoice",
                column: "SenderEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
