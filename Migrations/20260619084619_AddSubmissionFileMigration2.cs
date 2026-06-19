using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainee.api.Migrations
{
    /// <inheritdoc />
    public partial class AddSubmissionFileMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UploadedUserId",
                table: "SubmissionFiles",
                newName: "UploadedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UploadedByUserId",
                table: "SubmissionFiles",
                newName: "UploadedUserId");
        }
    }
}
