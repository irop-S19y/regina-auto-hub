using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoServicesRegina.Migrations
{
    /// <inheritdoc />
    public partial class FixCommentAndRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Services_AutoServiceId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "AutoServiceId",
                table: "Comments",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_AutoServiceId",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Ratings",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Comments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ServiceId",
                table: "Comments",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Services_ServiceId",
                table: "Comments",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Users_UserId",
                table: "Ratings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Services_ServiceId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Users_UserId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ServiceId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Comments",
                newName: "AutoServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                newName: "IX_Comments_AutoServiceId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Ratings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Comments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Services_AutoServiceId",
                table: "Comments",
                column: "AutoServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
