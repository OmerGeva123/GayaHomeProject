using Calculator.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverter()
    );
}); ;
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString(
            "DefaultConnection"
        )
    )
);

builder.Services.AddCors(options => {
options.AddPolicy("AllowAngular", policy => {
policy.WithOrigins("http://localhost:4200")// change to your Angular origin
                                           .AllowAnyHeader() 
                                           .AllowAnyMethod() 
                                           .AllowCredentials(); // only if you need cookies/auth; remove if not
                                                                }); });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();

app.UseAuthorization();

// Enable CORS using the named policy registered earlier.
// Ensure the policy is added via builder.Services.AddCors(...) before calling builder.Build().
app.UseCors("AllowAngular");

app.MapControllers();

app.Run();
