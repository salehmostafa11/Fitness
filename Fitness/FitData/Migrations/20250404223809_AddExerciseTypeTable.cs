using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitData.Migrations
{
    /// <inheritdoc />
    public partial class AddExerciseTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExerciseTypeId",
                table: "Video",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExerciseType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseType", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ExerciseType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Shoulder" },
                    { 2, "Back" },
                    { 3, "Chest" },
                    { 4, "Legs" },
                    { 5, "Arms" },
                    { 6, "Core" },
                    { 7, "Cardio" },
                    { 8, "Abs" },
                    { 9, "Cardio" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Video_ExerciseTypeId",
                table: "Video",
                column: "ExerciseTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Video_ExerciseType_ExerciseTypeId",
                table: "Video",
                column: "ExerciseTypeId",
                principalTable: "ExerciseType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Video_ExerciseType_ExerciseTypeId",
                table: "Video");

            migrationBuilder.DropTable(
                name: "ExerciseType");

            migrationBuilder.DropIndex(
                name: "IX_Video_ExerciseTypeId",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "ExerciseTypeId",
                table: "Video");
        }
    }
}
