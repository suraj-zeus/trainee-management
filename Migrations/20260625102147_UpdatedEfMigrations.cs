using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainee.api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEfMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessingJobs_SubmissionFiles_FileId",
                table: "ProcessingJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcessingJobs_Submissions_SubmissionId",
                table: "ProcessingJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignments_LearningTasks_LearningTaskId",
                table: "TaskAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignments_Mentors_MentorId",
                table: "TaskAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignments_Trainees_TraineeId",
                table: "TaskAssignments");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessingJobs_SubmissionFiles_FileId",
                table: "ProcessingJobs",
                column: "FileId",
                principalTable: "SubmissionFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessingJobs_Submissions_SubmissionId",
                table: "ProcessingJobs",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignments_LearningTasks_LearningTaskId",
                table: "TaskAssignments",
                column: "LearningTaskId",
                principalTable: "LearningTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignments_Mentors_MentorId",
                table: "TaskAssignments",
                column: "MentorId",
                principalTable: "Mentors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignments_Trainees_TraineeId",
                table: "TaskAssignments",
                column: "TraineeId",
                principalTable: "Trainees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessingJobs_SubmissionFiles_FileId",
                table: "ProcessingJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcessingJobs_Submissions_SubmissionId",
                table: "ProcessingJobs");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignments_LearningTasks_LearningTaskId",
                table: "TaskAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignments_Mentors_MentorId",
                table: "TaskAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskAssignments_Trainees_TraineeId",
                table: "TaskAssignments");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessingJobs_SubmissionFiles_FileId",
                table: "ProcessingJobs",
                column: "FileId",
                principalTable: "SubmissionFiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessingJobs_Submissions_SubmissionId",
                table: "ProcessingJobs",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignments_LearningTasks_LearningTaskId",
                table: "TaskAssignments",
                column: "LearningTaskId",
                principalTable: "LearningTasks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignments_Mentors_MentorId",
                table: "TaskAssignments",
                column: "MentorId",
                principalTable: "Mentors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskAssignments_Trainees_TraineeId",
                table: "TaskAssignments",
                column: "TraineeId",
                principalTable: "Trainees",
                principalColumn: "Id");
        }
    }
}
