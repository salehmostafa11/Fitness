using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitData.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedToPlanTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Level_LevelId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_NutritionPlans_AspNetUsers_NutritionistId",
                table: "NutritionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_NutritionPlans_AspNetUsers_TraineeId",
                table: "NutritionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_Level_LevelId",
                table: "Video");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NutritionPlans",
                table: "NutritionPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Level",
                table: "Level");

            migrationBuilder.RenameTable(
                name: "NutritionPlans",
                newName: "nutritionPlans");

            migrationBuilder.RenameTable(
                name: "Level",
                newName: "Levels");

            migrationBuilder.RenameIndex(
                name: "IX_NutritionPlans_TraineeId",
                table: "nutritionPlans",
                newName: "IX_nutritionPlans_TraineeId");

            migrationBuilder.RenameIndex(
                name: "IX_NutritionPlans_NutritionistId",
                table: "nutritionPlans",
                newName: "IX_nutritionPlans_NutritionistId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "nutritionPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_nutritionPlans",
                table: "nutritionPlans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Levels",
                table: "Levels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Levels_LevelId",
                table: "AspNetUsers",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_nutritionPlans_AspNetUsers_NutritionistId",
                table: "nutritionPlans",
                column: "NutritionistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_nutritionPlans_AspNetUsers_TraineeId",
                table: "nutritionPlans",
                column: "TraineeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Video_Levels_LevelId",
                table: "Video",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Levels_LevelId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_nutritionPlans_AspNetUsers_NutritionistId",
                table: "nutritionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_nutritionPlans_AspNetUsers_TraineeId",
                table: "nutritionPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_Video_Levels_LevelId",
                table: "Video");

            migrationBuilder.DropPrimaryKey(
                name: "PK_nutritionPlans",
                table: "nutritionPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Levels",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "nutritionPlans");

            migrationBuilder.RenameTable(
                name: "nutritionPlans",
                newName: "NutritionPlans");

            migrationBuilder.RenameTable(
                name: "Levels",
                newName: "Level");

            migrationBuilder.RenameIndex(
                name: "IX_nutritionPlans_TraineeId",
                table: "NutritionPlans",
                newName: "IX_NutritionPlans_TraineeId");

            migrationBuilder.RenameIndex(
                name: "IX_nutritionPlans_NutritionistId",
                table: "NutritionPlans",
                newName: "IX_NutritionPlans_NutritionistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NutritionPlans",
                table: "NutritionPlans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Level",
                table: "Level",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Level_LevelId",
                table: "AspNetUsers",
                column: "LevelId",
                principalTable: "Level",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionPlans_AspNetUsers_NutritionistId",
                table: "NutritionPlans",
                column: "NutritionistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionPlans_AspNetUsers_TraineeId",
                table: "NutritionPlans",
                column: "TraineeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Video_Level_LevelId",
                table: "Video",
                column: "LevelId",
                principalTable: "Level",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
