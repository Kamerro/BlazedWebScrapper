using BlazedWebScrapper.Data;
using BlazedWebScrapper.Entities;
using BlazedWebScrapper.Data.Classes.Factories;
using BlazedWebScrapper.Data.Classes.Services;
using BlazedWebScrapper.Data.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<WebScrapperDbContext>(
	options => options.UseSqlServer(builder.Configuration.GetConnectionString("WebScrapper")));
builder.Services.AddScoped<FlightService>();
builder.Services.AddSingleton<IBasicWebScrapperSite,WebScrapperImplementation>();
builder.Services.AddSingleton<IFactorySearcher, FactorySearcher>();
builder.Services.AddTransient<BookService>();

builder.Services.AddScoped<FlightService>();
builder.Services.AddScoped<EmailSender>();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
