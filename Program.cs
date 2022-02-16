global using Microsoft.EntityFrameworkCore;
global using PaginationNET6.Data;
global using PaginationNET6.Models;

var builder = WebApplication.CreateBuilder(args);


//Config Controller
builder.Services.AddControllers();

//Config Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Config SQL
builder.Services.AddDbContext<DataContext>(optioons =>
{
    optioons.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //Run Swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Run Https
app.UseHttpsRedirection();

app.UseAuthorization();

//Run Controller
app.MapControllers();

app.Run();
