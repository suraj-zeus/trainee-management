using NSwag.AspNetCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.IdentityModel.Tokens;
// using System.Text;


using Trainee.api.Services;
using Trainee.api.DatabaseContext;
using Trainee.api.Repositories;
using Trainee.api.Models;

var builder = WebApplication.CreateBuilder(args);



// db config
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);



// authentication config
// builder.Services
//     .AddAuthentication(options =>
//         {
//             options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//             options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//         }
//     )
//     .AddJwtBearer(options =>
//         {
//             options.TokenValidationParameters = new TokenValidationParameters
//             {
//                 ValidateIssuer = true,
//                 ValidateAudience = true,
//                 ValidateLifetime = true,
//                 ValidateIssuerSigningKey = true,
//                 ValidIssuer = builder.Configuration["Jwt:Issuer"],
//                 ValidAudience = builder.Configuration["Jwt:Audience"],
//                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
//             };
//         }
//     );



// add controllers
builder.Services.AddControllers(options =>
{
    options.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider());
});


// add dependency injection config for service layer and repo layer
builder.Services.AddScoped<ITraineeService, TraineeService>();
builder.Services.AddScoped<ITraineeRepository, TraineeRepository>();

// openapi (swagger) config
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "v1";
    config.Title = "Training Management Apis";
});


var app = builder.Build();



// default admin data seeding
using (var scope = app.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Users.Any())
    {
        var admin = new UserModel
        {
            Username = "admin",
            Email = "admin@gmail.com",
            PasswordHash = "",
            Role = "Admin",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var hasher = new PasswordHasher<UserModel>();
        string hashedPassword = hasher.HashPassword(admin, "admin@123");
        admin.PasswordHash = hashedPassword;

        Console.WriteLine("Seeding user: " + admin);
        db.Users.Add(admin);
        db.SaveChanges();
    }
}



app.MapGet("/", () => "Hello World!");


app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

// swagger
app.UseOpenApi();
app.UseSwaggerUi();

app.Run();
