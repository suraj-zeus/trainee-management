using Microsoft.EntityFrameworkCore;
using Trainee.api.Models;

namespace Trainee.api.DatabaseContext;

public class AppDbContext : DbContext
{


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<TraineeModel> Trainees {get; set;}
}