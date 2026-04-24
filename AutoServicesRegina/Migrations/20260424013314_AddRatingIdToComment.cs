using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoServicesRegina.Migrations
{
    /// <inheritdoc />
    public partial class AddRatingIdToComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RatingId",
                table: "Comments",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_RatingId",
                table: "Comments",
                column: "RatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Ratings_RatingId",
                table: "Comments",
                column: "RatingId",
                principalTable: "Ratings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Ratings_RatingId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_RatingId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "RatingId",
                table: "Comments");
        }
    }
}
