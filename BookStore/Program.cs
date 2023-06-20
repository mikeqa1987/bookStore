using System.Text;
using System.Text.Json.Serialization;
using BookStore.Controllers;
using BookStore.DB;
using BookStore.DB.Model;
using BookStore.MappingProfiles;
using BookStore.Repositories;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");


// Add services to the container.
builder.Services.AddDbContext<BookContext>(options => options.UseSqlite(connection));
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddControllers()
    .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling
         = ReferenceLoopHandling.Ignore);
builder.Services.AddAutoMapper(typeof(BookMappings));
//CORS
builder.Services.AddCors(options => options.AddPolicy("AllowAny", builder => builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod())
);

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//app.UseMiddleware<RoutingMiddleware>();
app.UseCors("AllowAny");

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