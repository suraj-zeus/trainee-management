using Microsoft.EntityFrameworkCore;
using Trainee.api.Models;

namespace Trainee.api.DatabaseContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<TraineeModel> Trainees { get; set; }

    public DbSet<UserModel> Users { get; set; }

    public DbSet<MentorModel> Mentors { get; set; }

    public DbSet<LearningTaskModel> LearningTasks { get; set; }

    public DbSet<TaskAssignmentModel> TaskAssignments {get; set; }

    public DbSet<SubmissionModel> Submissions {get; set; }

    public DbSet<ReviewModel> Reviews {get; set;}
    


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<TaskAssignmentModel>()
            .HasOne(ta => ta.Trainee)          // Each TaskAssignment has one Trainee
            .WithMany(t => t.TaskAssignments)        // Each Trainee has many TaskAssignments
            .HasForeignKey(ta => ta.TraineeId) // Foreign key in TaskAssignment table
            .OnDelete(DeleteBehavior.Cascade); // Cascade Null
 
        modelBuilder.Entity<TaskAssignmentModel>()
            .HasOne(ta => ta.Mentor)          // Each TaskAssignment has one Mentor
            .WithMany(m => m.TaskAssignments)        // Each Mentor has many TaskAssignments
            .HasForeignKey(ta => ta.MentorId) // Foreign key in TaskAssignment table
            .OnDelete(DeleteBehavior.Cascade); // Cascade Null
 
        modelBuilder.Entity<TaskAssignmentModel>()
            .HasOne(ta => ta.LearningTask)          // Each TaskAssignemnt has one LearningTask
            .WithMany(t => t.TaskAssignments)        // Each LearningTask has many TaskAssignments
            .HasForeignKey(ta => ta.LearningTaskId) // Foreign key in Task Assignment table
            .OnDelete(DeleteBehavior.Cascade); // Cascade Null

        modelBuilder.Entity<SubmissionModel>()
            .HasOne(s => s.TaskAssignment)
            .WithMany(ta => ta.Submissions)
            .HasForeignKey(s => s.TaskAssignmentId);

        modelBuilder.Entity<ReviewModel>()
            .HasOne(r => r.Submission)
            .WithMany(s => s.Reviews)
            .HasForeignKey(r => r.SubmissionId);
 
        modelBuilder.Entity<ReviewModel>()
            .HasOne(r => r.Mentor)
            .WithMany(s => s.Reviews)
            .HasForeignKey(r => r.MentorId);
    }
}