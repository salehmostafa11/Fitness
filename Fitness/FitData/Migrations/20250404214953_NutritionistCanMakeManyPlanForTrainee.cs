using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitData.Migrations
{
    /// <inheritdoc />
    public partial class NutritionistCanMakeManyPlanForTrainee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_nutritionPlans_TraineeId",
                table: "nutritionPlans");

            migrationBuilder.CreateIndex(
                name: "IX_nutritionPlans_TraineeId",
                table: "nutritionPlans",
                column: "TraineeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_nutritionPlans_TraineeId",
                table: "nutritionPlans");

            migrationBuilder.CreateIndex(
                name: "IX_nutritionPlans_TraineeId",
                table: "nutritionPlans",
                column: "TraineeId",
                unique: true);
        }
    }
}
