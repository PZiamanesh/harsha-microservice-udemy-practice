var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers()
.AddJsonOptions(ops =>
{
    ops.JsonSerializerOptions.PropertyNameCaseInsensitive = true; // json -> c#
    ops.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; // c# -> json
    ops.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // string <-> enum
});

builder.Services.AddProblemDetails();

builder.Services.AddCoreLayerServices(configuration);
builder.Services.AddRepositories(configuration);
builder.Services.AddInfrastructureLayerServices(configuration);

builder.Services.AddCors(ops =>
{
    ops.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});




var app = builder.Build();

app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseGlobalExceptionHandlerMiddleware();
}

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
