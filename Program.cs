using NSwag.AspNetCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

using NSwag;
using NSwag.Generation.Processors.Security;

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


// cors configs
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});


// logging configs
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// make apis route lowercase
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);


// authentication config
builder.Services
    .AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    )
    .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
            };
        }
    );

builder.Services.AddAuthorization();


// add controllers
builder.Services.AddControllers(options =>
{
    options.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider());
}).AddJsonOptions(options =>
{
    // consider enum as string
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


// add dependency injection config for service layer and repo layer
builder.Services.AddScoped<ITraineeRepository, TraineeRepository>();
builder.Services.AddScoped<ILearningTaskRepository, LearningTaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMentorRepository, MentorRepository>();

builder.Services.AddScoped<ITraineeService, TraineeService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMentorService, MentorService>();
builder.Services.AddScoped<ILearningTaskService, LearningTaskService>();

// openapi (swagger) config
builder.Services.AddOpenApiDocument(config =>
    {
        config.DocumentName = "v1";
        config.Title = "Training Management Apis";

        // add  jwt secuity options in swagger
        config.AddSecurity("JWT", new OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Name = "Authorization",
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Add jwt token for protected routes in this format : Bearer <jwt_token>"
        });

        config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
    }
);


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
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        var hasher = new PasswordHasher<UserModel>();
        string hashedPassword = hasher.HashPassword(admin, "admin@123");
        admin.PasswordHash = hashedPassword;

        Console.WriteLine("Seeding user: " + admin);
        db.Users.Add(admin);
        db.SaveChanges();
    }
}

// cors
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

// default controller
app.MapGet("/", () => "Hello World!");

// security
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

// swagger
app.UseOpenApi();
app.UseSwaggerUi();

app.Run();
