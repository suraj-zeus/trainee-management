using NSwag.AspNetCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http.Features;
using System.Text.Json.Serialization;

using NSwag;
using NSwag.Generation.Processors.Security;

using Trainee.api.Services;
using Trainee.api.DatabaseContext;
using Trainee.api.Repositories;
using Trainee.api.Models;
using Trainee.api.Exceptions;
using Trainee.api.Configurations;
using DotNetEnv;

// load .env file
DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// read env variables
builder.Configuration.AddEnvironmentVariables();


// exception handling 
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails(); 



// bind custom configurations
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(JwtConfig.SectionName));
builder.Services.Configure<AdminDefaultUserConfig>(builder.Configuration.GetSection(AdminDefaultUserConfig.SectionName));
builder.Services.Configure<FileStorageConfig>(builder.Configuration.GetSection(FileStorageConfig.SectionName));
builder.Services.Configure<RedisConfig>(builder.Configuration.GetSection(RedisConfig.SectionName));
builder.Services.Configure<RabbitMqConfig>(builder.Configuration.GetSection(RabbitMqConfig.SectionName));


// db config
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);


// redis config
RedisConfig redisConfig = builder.Configuration.GetSection(RedisConfig.SectionName).Get<RedisConfig>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConfig.ConnectionString;
    options.InstanceName = redisConfig.InstanceName;
});

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


// request(kestrel) size or form data size configurations 
FileStorageConfig fileStorageConfig = builder.Configuration.GetSection(FileStorageConfig.SectionName).Get<FileStorageConfig>();



// for data size
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = fileStorageConfig.MaxFileSizeBytes;
});


// to register the context accessor
builder.Services.AddHttpContextAccessor();

// logging configs
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// make apis route lowercase
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);


// authentication config
var jwtSettings = builder.Configuration.GetSection(JwtConfig.SectionName).Get<JwtConfig>();

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
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)!)
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
    // ignore cycle globally
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});


// add dependency injection config for service layer and repo layer
builder.Services.AddScoped<ITraineeRepository, TraineeRepository>();
builder.Services.AddScoped<ILearningTaskRepository, LearningTaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMentorRepository, MentorRepository>();
builder.Services.AddScoped<ITaskAssignmentRepository, TaskAssignmentRepository>();
builder.Services.AddScoped<ISubmissionRepository, SubmissionRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<ISubmissionFileRepository, SubmissionFileRepository>();


builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();
builder.Services.AddScoped<IRedisService, RedisService>();
builder.Services.AddScoped<IDbSeederService, DbSeederService>();
builder.Services.AddScoped<ITraineeService, TraineeService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMentorService, MentorService>();
builder.Services.AddScoped<ILearningTaskService, LearningTaskService>();
builder.Services.AddScoped<ITaskAssignmentService, TaskAssignmentService>();
builder.Services.AddScoped<ISubmissionService, SubmissionService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ISubmissionFileService, SubmissionFileService>();
builder.Services.AddScoped<IRabbitMqService, RabbitMqService>();


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



// Use the exception handler
app.UseExceptionHandler(); 

// default admin data seeding
using (var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;

    var db = services.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();

    IDbSeederService dbSeeder = services.GetRequiredService<IDbSeederService>();
    await dbSeeder.SeedAdminUserAsync();
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
