using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoServicesRegina.Migrations
{
    /// <inheritdoc />
    public partial class AddStripeSessionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StripePaymentId",
                table: "Donations",
                newName: "StripeSessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StripeSessionId",
                table: "Donations",
                newName: "StripePaymentId");
        }
    }
}
