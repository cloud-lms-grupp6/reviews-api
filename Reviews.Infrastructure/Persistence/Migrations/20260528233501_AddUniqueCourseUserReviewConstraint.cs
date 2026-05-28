using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reviews.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueCourseUserReviewConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CourseId_UserId",
                table: "Reviews",
                columns: new[] { "CourseId", "UserId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reviews_CourseId_UserId",
                table: "Reviews");
        }
    }
}
