using NSwag.AspNetCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Trainee.api.Services;
using Trainee.api.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Trainee.api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// add controllers
builder.Services.AddControllers(options =>
{
    options.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider());
});

// in memory db config
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TraineeManagementDb"));

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

app.MapGet("/", () => "Hello World!");


app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

// swagger
app.UseOpenApi();
app.UseSwaggerUi();

app.Run();
