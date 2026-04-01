using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoServicesRegina.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ratings_ServiceId",
                table: "Ratings");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ServiceId_UserId",
                table: "Ratings",
                columns: new[] { "ServiceId", "UserId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ratings_ServiceId_UserId",
                table: "Ratings");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ServiceId",
                table: "Ratings",
                column: "ServiceId");
        }
    }
}
