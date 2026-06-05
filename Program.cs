using NSwag.AspNetCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Trainee.api.Interfaces;
using Trainee.api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider());
});

builder.Services.AddSingleton<ITraineeService, TraineeService>();

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
