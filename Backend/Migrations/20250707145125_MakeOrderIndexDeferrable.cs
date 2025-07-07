using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitSync.Migrations
{
    /// <inheritdoc />
    public partial class MakeOrderIndexDeferrable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        -- 1) обриши индекс ако постоји
        DROP INDEX IF EXISTS ""IX_ExercisePlanItems_Plan_Order_Unique"";

        -- 2) обриши констреинт ако је остао од раније
        ALTER TABLE ""ExercisePlanItems""
        DROP CONSTRAINT IF EXISTS ""UQ_ExercisePlanItems_Plan_Order"";

        -- 3) додај DEFERRABLE констреинт
        ALTER TABLE ""ExercisePlanItems""
        ADD CONSTRAINT ""UQ_ExercisePlanItems_Plan_Order""
          UNIQUE (""ExercisePlanId"", ""Order"")
          DEFERRABLE INITIALLY DEFERRED;
        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        ALTER TABLE ""ExercisePlanItems""
        DROP CONSTRAINT IF EXISTS ""UQ_ExercisePlanItems_Plan_Order"";

        CREATE UNIQUE INDEX IF NOT EXISTS ""IX_ExercisePlanItems_Plan_Order_Unique""
        ON ""ExercisePlanItems"" (""ExercisePlanId"", ""Order"");
        ");
        }


    }
}
