using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MovieApi.Models;
using MovieApi.Services.Characters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//
//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Version = "v1",
//        Title = "(yet another) Guitar API",
//        Description = "YET ANOTHER GUITAR API",
//        Contact = new OpenApiContact
//        {
//            Name = "Sean Skinner",
//            Url = new Uri("https://gitlab.com/SeanSkinner")
//        },
//        License = new OpenApiLicense
//        {
//            Name = "MIT 2022",
//            Url = new Uri("https://opensource.org/license/mit/")
//        }
//    });
//    options.IncludeXmlComments(xmlPath);
//});

builder.Services.AddTransient<ICharacterService, CharacterService>();
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

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
