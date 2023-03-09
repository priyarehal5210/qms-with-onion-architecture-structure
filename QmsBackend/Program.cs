using ApplicationLayer.Dtos;
using ApplicationLayer.Validations;
using DomainLayer.Common;
using DomainLayer.Entities;
using DomainLayer.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastucture;
using Infrastucture.Jwt;
using Infrastucture.Mapping;
using Infrastucture.Repository.Implementations;
using Infrastucture.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwagger>();

builder.Services.AddTransient<IValidator<RegisteredUserDto>, RegistrationValidation>();
builder.Services.AddTransient<IValidator<AssignTasksDto>, AssignTaskValidation>();
builder.Services.AddTransient<IValidator<TasksDto>, TaskValidation>();
builder.Services.AddTransient<IValidator<UserSuccessDto>, UserStatusValidation>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(ProfileMapping));
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddSwaggerGen();
//cores
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
   builder =>
   {
       builder.WithOrigins("http://localhost:4200")
.AllowAnyOrigin()
.AllowAnyHeader()
.AllowAnyMethod();
   });
});
//implementing jwt
var appsettingssection = builder.Configuration.GetSection("Appsetting");
builder.Services.Configure<Appsetting>(appsettingssection);
var appsetting = appsettingssection.Get<Appsetting>();
var key = Encoding.ASCII.GetBytes(appsetting.Secret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("MyPolicy");

app.MapControllers();

app.Run();
