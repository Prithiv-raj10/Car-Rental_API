using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//clear inbuild loggers
builder.Logging.ClearProviders();

// add log4net
builder.Logging.AddLog4Net();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDbConnection"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 1;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
});
// reading key
var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");
builder.Services.AddAuthentication(u =>
{
    u.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    u.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(u =>
{
    u.RequireHttpsMetadata = false;
    u.SaveToken = true;
    u.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddCors();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<ICarListRepository,CarListRepository>();

builder.Services.AddTransient<IAuthRepository, AuthRepository>();

builder.Services.AddTransient<IBookingRepository, BookingRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IPaymentRepository, PaymentRepository>();
builder.Services.AddTransient<IAuthBLL, AuthBLL>();
builder.Services.AddTransient<IBookingBLL,BookingBLL>();
builder.Services.AddTransient<ICarListBLL,CarListBLL>();
builder.Services.AddTransient<IOrderBLL,OrderBLL>();
builder.Services.AddTransient<IPaymentBLL,PaymentBLL>();
builder.Services.AddTransient<CarListBLL>();
builder.Services.AddTransient<AuthBLL>();
builder.Services.AddTransient<BookingBLL>();
builder.Services.AddTransient<OrderBLL>();
builder.Services.AddTransient<PaymentBLL>();
builder.Services.AddSwaggerGen();

log4net.Config.XmlConfigurator.Configure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(o => o.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();



app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
