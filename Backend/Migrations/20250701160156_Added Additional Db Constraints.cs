using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitSync.Migrations
{
    /// <inheritdoc />
    public partial class AddedAdditionalDbConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Workouts_UserId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutExercises_WorkoutId",
                table: "WorkoutExercises");

            migrationBuilder.DropIndex(
                name: "IX_ExercisePlans_UserId",
                table: "ExercisePlans");

            migrationBuilder.DropIndex(
                name: "IX_ExercisePlanItems_ExercisePlanId",
                table: "ExercisePlanItems");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_UserId_Date_Unique",
                table: "Workouts",
                columns: new[] { "UserId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_WorkoutExercises_Workout_Order",
                table: "WorkoutExercises",
                columns: new[] { "WorkoutId", "OrderInWorkout" },
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_WorkoutExercises_PositiveValues",
                table: "WorkoutExercises",
                sql: "\"Sets\" > 0 AND \"Reps\" > 0 AND \"Weight\" > 0 AND \"RestSeconds\" > 0 AND \"OrderInWorkout\" > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_PersonalRecords_PositiveValuesAndDate",
                table: "PersonalRecords",
                sql: "\"MaxWeight\" > 0 AND \"Reps\" > 0");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_Name_Type",
                table: "Exercises",
                columns: new[] { "Name", "Type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExercisePlans_UserId_Name",
                table: "ExercisePlans",
                columns: new[] { "UserId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExercisePlanItems_ExercisePlanId_ExerciseId",
                table: "ExercisePlanItems",
                columns: new[] { "ExercisePlanId", "ExerciseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExercisePlanItems_Plan_Order_Unique",
                table: "ExercisePlanItems",
                columns: new[] { "ExercisePlanId", "Order" },
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_ExercisePlanItems_PositiveValues",
                table: "ExercisePlanItems",
                sql: "\"Order\" > 0 AND \"Sets\" > 0 AND \"Reps\" > 0");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Workouts_UserId_Date_Unique",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "UX_WorkoutExercises_Workout_Order",
                table: "WorkoutExercises");

            migrationBuilder.DropCheckConstraint(
                name: "CK_WorkoutExercises_PositiveValues",
                table: "WorkoutExercises");

            migrationBuilder.DropCheckConstraint(
                name: "CK_PersonalRecords_PositiveValuesAndDate",
                table: "PersonalRecords");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_Name_Type",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_ExercisePlans_UserId_Name",
                table: "ExercisePlans");

            migrationBuilder.DropIndex(
                name: "IX_ExercisePlanItems_ExercisePlanId_ExerciseId",
                table: "ExercisePlanItems");

            migrationBuilder.DropIndex(
                name: "IX_ExercisePlanItems_Plan_Order_Unique",
                table: "ExercisePlanItems");

            migrationBuilder.DropCheckConstraint(
                name: "CK_ExercisePlanItems_PositiveValues",
                table: "ExercisePlanItems");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_UserId",
                table: "Workouts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_WorkoutId",
                table: "WorkoutExercises",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_ExercisePlans_UserId",
                table: "ExercisePlans",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExercisePlanItems_ExercisePlanId",
                table: "ExercisePlanItems",
                column: "ExercisePlanId");
        }
    }
}
