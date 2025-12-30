using Microsoft.EntityFrameworkCore;
using SaleApi.Data;
using SaleApi.Repositories;
using SaleApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDonerService, DonerService>();
builder.Services.AddScoped<IGiftService, GiftService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IGiftRepository, GiftRepository>();
builder.Services.AddScoped<IDonerRepository, DonerRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddDbContext<SaleContextDB>(options =>
        options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;DataBase=SaleDB;Integrated Security=SSPI;Persist Security Info=False;TrustServerCertificate=True;"));

///להתעלם ממעגליות
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

//srv2\\pupils
//register repository and service with matching types
//builder.Services.AddScoped<SaleApi.Repositories.DonerRepository>();


//builder.Services.AddScoped<SaleApi.Services.DonerService>();

//builder.Services.AddScoped<SaleApi.Repositories.GiftRepository>();
//builder.Services.AddScoped<SaleApi.Services.GiftService>();
//builder.Services.AddScoped<SaleApi.Repositories.CategoryRepository>();
//builder.Services.AddScoped<SaleApi.Services.CategoryService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
