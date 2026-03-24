using Microsoft.EntityFrameworkCore;
using SaleApi.Data;
using SaleApi.Repositories;
using SaleApi.Services;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthentication(options =>
//{
//    // הגדרת ברירת המחדל לשימוש בטוקן מסוג JWT
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        // וודאי שהשמות כאן תואמים בדיוק למה שכתוב ב-appsettings.json שלך
//        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
//        ValidAudience = builder.Configuration["JwtSettings:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
//    };
//});

// Add services to the container.

builder.Services.AddControllers();


// 1. הגדרת הפוליסה
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy",
 policy => policy.AllowAnyOrigin()  
                        .AllowAnyMethod()   
                        .AllowAnyHeader());
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDonerService, DonerService>();
builder.Services.AddScoped<IGiftService, GiftService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBagService, BagService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<SaleApi.Services.TokenService>();
builder.Services.AddScoped<IRandomService, RandomService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IEmailService, EmailService>();


builder.Services.AddScoped<IGiftRepository, GiftRepository>();
builder.Services.AddScoped<IDonerRepository, DonerRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBagRepository, BagRepository>();
builder.Services.AddScoped<IRandomRepository, RandomRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();


builder.Services.AddDbContext<SaleContextDB>(options =>
        options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;DataBase=SaleDB;Integrated Security=SSPI;Persist Security Info=False;TrustServerCertificate=True;"));
//builder.Services.AddDbContext<SaleContextDB>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
///להתעלם ממעגליות
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

//srv2\\pupils
//(localdb)\\MSSQLLocalDB
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
app.UseRouting(); 
app.UseCors("AngularPolicy");

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();

app.Run();
