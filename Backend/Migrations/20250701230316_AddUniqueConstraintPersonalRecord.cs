using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitSync.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintPersonalRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PersonalRecords_ExerciseId",
                table: "PersonalRecords");

            migrationBuilder.CreateIndex(
                name: "UX_PersonalRecord_Exercise_User",
                table: "PersonalRecords",
                columns: new[] { "ExerciseId", "UserId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PersonalRecords_UserId",
                table: "PersonalRecords");

            migrationBuilder.DropIndex(
                name: "UX_PersonalRecord_Exercise_User",
                table: "PersonalRecords");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalRecords_ExerciseId",
                table: "PersonalRecords",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "UX_PersonalRecord_User_Exercise",
                table: "PersonalRecords",
                columns: new[] { "UserId", "ExerciseId" },
                unique: true);
        }
    }
}
