using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Migrations
{
    /// <inheritdoc />
    public partial class RenameDailyPlans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Layers_DailyPlans_DailyPlanId",
                table: "Layers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_DailyPlans_DailyPlanId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DailyPlans",
                table: "DailyPlans");

            migrationBuilder.RenameTable(
                name: "DailyPlans",
                newName: "NewDailyPlans");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewDailyPlans",
                table: "NewDailyPlans",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Layers_NewDailyPlans_DailyPlanId",
                table: "Layers",
                column: "DailyPlanId",
                principalTable: "NewDailyPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_NewDailyPlans_DailyPlanId",
                table: "Tasks",
                column: "DailyPlanId",
                principalTable: "NewDailyPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Layers_NewDailyPlans_DailyPlanId",
                table: "Layers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_NewDailyPlans_DailyPlanId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewDailyPlans",
                table: "NewDailyPlans");

            migrationBuilder.RenameTable(
                name: "NewDailyPlans",
                newName: "DailyPlans");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DailyPlans",
                table: "DailyPlans",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Layers_DailyPlans_DailyPlanId",
                table: "Layers",
                column: "DailyPlanId",
                principalTable: "DailyPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_DailyPlans_DailyPlanId",
                table: "Tasks",
                column: "DailyPlanId",
                principalTable: "DailyPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
