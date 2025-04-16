using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitData.Migrations
{
    /// <inheritdoc />
    public partial class AddMealTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MealTypeId",
                table: "nutritionPlans",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MealType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealType", x => x.Id);
                });
                migrationBuilder.InsertData(
                    table: "MealType",
                    columns: new[] { "Id", "Name" },
                    values: new object[,]
                    {
                                        { 1, "Breakfast" },
                                        { 2, "Dinner" },
                                        { 3, "Lunch" },
                                        { 4, "Snack" }
                        });

            migrationBuilder.CreateIndex(
                name: "IX_nutritionPlans_MealTypeId",
                table: "nutritionPlans",
                column: "MealTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_nutritionPlans_MealType_MealTypeId",
                table: "nutritionPlans",
                column: "MealTypeId",
                principalTable: "MealType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_nutritionPlans_MealType_MealTypeId",
                table: "nutritionPlans");

            migrationBuilder.DropTable(
                name: "MealType");

            migrationBuilder.DropIndex(
                name: "IX_nutritionPlans_MealTypeId",
                table: "nutritionPlans");

            migrationBuilder.DropColumn(
                name: "MealTypeId",
                table: "nutritionPlans");
        }
    }
}
