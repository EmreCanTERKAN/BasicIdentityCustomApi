using BasicIdentityCustomApi.Data;
using BasicIdentityCustomApi.Managers;
using BasicIdentityCustomApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



// Bu ayardan �nce appsetting.jsonda JWT tan�mlamas� yap�l�r. 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                                  .AddJwtBearer(options =>
                                  {
                                      options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                                      {
                                          // appsettings.json' JWT ye yazd���m�z issuer ile bu e�le�sin mi ? bunun kontroll�n� yapar.
                                          ValidateIssuer = true,
                                          // bu issuer hangisi olacak appsettingsteki de�er Jwt ismi ile Issuer
                                          ValidIssuer = builder.Configuration["Jwt:Issuer"],
                                          // Yukar�daki i�lemleri Audience i�inde yap�l�r.
                                          ValidateAudience = true,
                                          ValidAudience = builder.Configuration["Jwt:Audience"],
                                          // Zaman� var m� yok mu kontroller ediyim mi ?
                                          ValidateLifetime = true,
                                          // appsettingdeki keye bak�l�r.
                                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
                                          // Bu kodlamalar bittikten sonra Bir Jwt dosyas� a��l�p i�erisine JwtHelper class� olu�turulur.
                                      };

                                  });

builder.Services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserManager>();
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

app.MapControllers();

app.Run();
