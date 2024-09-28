using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Migrations
{
    /// <inheritdoc />
    public partial class AddDailyPlanwithsection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "DailyPlanId",
                table: "Tasks",
                newName: "SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_DailyPlanId",
                table: "Tasks",
                newName: "IX_Tasks_SectionId");

            migrationBuilder.RenameColumn(
                name: "DailyPlanId",
                table: "Layers",
                newName: "SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Layers_DailyPlanId",
                table: "Layers",
                newName: "IX_Layers_SectionId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Tasks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "Tasks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Tasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Tasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "Tasks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Layers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Layers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "Layers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Layers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Layers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Layers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "Layers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "DailyPlans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "DailyPlans",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "DailyPlans",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "DailyPlans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DailyPlans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "DailyPlans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "DailyPlans",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DailyPlans",
                table: "DailyPlans",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DailyPlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalTaskDuration = table.Column<int>(type: "int", nullable: false),
                    TotalLayerDuration = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_DailyPlans_DailyPlanId",
                        column: x => x.DailyPlanId,
                        principalTable: "DailyPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sections_DailyPlanId",
                table: "Sections",
                column: "DailyPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Layers_Sections_SectionId",
                table: "Layers",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Sections_SectionId",
                table: "Tasks",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Layers_Sections_SectionId",
                table: "Layers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Sections_SectionId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DailyPlans",
                table: "DailyPlans");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Layers");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Layers");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "Layers");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Layers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Layers");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Layers");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "Layers");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "DailyPlans");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "DailyPlans");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "DailyPlans");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "DailyPlans");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DailyPlans");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "DailyPlans");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "DailyPlans");

            migrationBuilder.RenameTable(
                name: "DailyPlans",
                newName: "NewDailyPlans");

            migrationBuilder.RenameColumn(
                name: "SectionId",
                table: "Tasks",
                newName: "DailyPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_SectionId",
                table: "Tasks",
                newName: "IX_Tasks_DailyPlanId");

            migrationBuilder.RenameColumn(
                name: "SectionId",
                table: "Layers",
                newName: "DailyPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_Layers_SectionId",
                table: "Layers",
                newName: "IX_Layers_DailyPlanId");

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
    }
}
