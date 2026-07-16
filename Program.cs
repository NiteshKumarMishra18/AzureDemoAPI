using AzureDemoAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("ReactPolicy", policy =>
//    {
//        policy.WithOrigins("https://azurereact12345-fta4chbcbch9cga6.centralindia-01.azurewebsites.net")
//              .AllowAnyHeader()
//              .AllowAnyMethod();
//    });
//});
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
// ci/cd test
// IMPORTANT
app.UseCors("ReactPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

// environemnt deployment test