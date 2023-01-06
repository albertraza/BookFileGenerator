using System.Reflection;
using Books.Extensions;
using Books.Models;
using Books.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

// Add caching
builder.Services.AddMemoryCache();

builder.Services.AddFileParsers();
builder.Services.AddTransient<ICacheService, CacheService>();
builder.Services.AddTransient(typeof(ICSVContentGenerator<>), typeof(CSVContentGenerator<>));
builder.Services.AddTransient<IBookService, BookService>();

// add automapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// getting books api config
var bookConfig = builder.Configuration.GetSection(nameof(BookApiConfig)).Get<BookApiConfig>();
builder.Services.AddSingleton(bookConfig);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors(config =>
{
    config.AllowAnyOrigin();
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
