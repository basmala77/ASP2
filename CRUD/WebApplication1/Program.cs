using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApplication1.Data;
using System.Globalization;
using WebApplication1.Middlewares;

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbcontext>(builder => builder.UseSqlServer("Data Source=.;Initial Catalog=EFC;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();
//app.UseMiddleware<ProfilingMiddleware>();   
app.MapControllers();

app.Run();
