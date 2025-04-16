using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitData.Migrations
{
    /// <inheritdoc />
    public partial class RelationBetweenVideosAndTrainner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrainnerId",
                table: "Video",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Video_TrainnerId",
                table: "Video",
                column: "TrainnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Video_AspNetUsers_TrainnerId",
                table: "Video",
                column: "TrainnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Video_AspNetUsers_TrainnerId",
                table: "Video");

            migrationBuilder.DropIndex(
                name: "IX_Video_TrainnerId",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "TrainnerId",
                table: "Video");
        }
    }
}
