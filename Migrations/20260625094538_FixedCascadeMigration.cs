using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trainee.api.Migrations
{
    /// <inheritdoc />
    public partial class FixedCascadeMigration : Migration
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
                name: "FK_Reviews_Mentors_MentorId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Submissions_SubmissionId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionFiles_Submissions_SubmissionId",
                table: "SubmissionFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_TaskAssignments_TaskAssignmentId",
                table: "Submissions");

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
                name: "FK_Reviews_Mentors_MentorId",
                table: "Reviews",
                column: "MentorId",
                principalTable: "Mentors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Submissions_SubmissionId",
                table: "Reviews",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionFiles_Submissions_SubmissionId",
                table: "SubmissionFiles",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_TaskAssignments_TaskAssignmentId",
                table: "Submissions",
                column: "TaskAssignmentId",
                principalTable: "TaskAssignments",
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
                name: "FK_Reviews_Mentors_MentorId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Submissions_SubmissionId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_SubmissionFiles_Submissions_SubmissionId",
                table: "SubmissionFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_TaskAssignments_TaskAssignmentId",
                table: "Submissions");

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
                name: "FK_Reviews_Mentors_MentorId",
                table: "Reviews",
                column: "MentorId",
                principalTable: "Mentors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Submissions_SubmissionId",
                table: "Reviews",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubmissionFiles_Submissions_SubmissionId",
                table: "SubmissionFiles",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_TaskAssignments_TaskAssignmentId",
                table: "Submissions",
                column: "TaskAssignmentId",
                principalTable: "TaskAssignments",
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
    }
}
