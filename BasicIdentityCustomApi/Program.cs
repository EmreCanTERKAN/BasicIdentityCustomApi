using BasicIdentityCustomApi.Data;
using BasicIdentityCustomApi.Managers;
using BasicIdentityCustomApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



// Bu ayardan önce appsetting.jsonda JWT tanýmlamasý yapýlýr. 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                                  .AddJwtBearer(options =>
                                  {
                                      options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                                      {
                                          // appsettings.json' JWT ye yazdýðýmýz issuer ile bu eþleþsin mi ? bunun kontrollünü yapar.
                                          ValidateIssuer = true,
                                          // bu issuer hangisi olacak appsettingsteki deðer Jwt ismi ile Issuer
                                          ValidIssuer = builder.Configuration["Jwt:Issuer"],
                                          // Yukarýdaki iþlemleri Audience içinde yapýlýr.
                                          ValidateAudience = true,
                                          ValidAudience = builder.Configuration["Jwt:Audience"],
                                          // Zamaný var mý yok mu kontroller ediyim mi ?
                                          ValidateLifetime = true,
                                          // appsettingdeki keye bakýlýr.
                                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
                                          // Bu kodlamalar bittikten sonra Bir Jwt dosyasý açýlýp içerisine JwtHelper classý oluþturulur.
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
