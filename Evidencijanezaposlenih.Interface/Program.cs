using EvidencijaNezaposlenih.PoslovnaLogika.Interfejsi;
using EvidencijaNezaposlenih.PoslovnaLogika.Validacija;
using EvidencijaNezaposlenih.Repozitorijum.Context;
using EvidencijaNezaposlenih.Repozitorijum.Interfejsi;
using EvidencijaNezaposlenih.Repozitorijum.Repozitorijumi;
using EvidencijaNezaposlenih.Servisi.Interfejsi;
using EvidencijaNezaposlenih.Servisi.Servisi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<EvidencijaNezaposlenihDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EvidencijaNezaposlenihDBContext")
    ).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddScoped<INezaposleniRepozitorijum, NezaposleniRepozitorujum>();
builder.Services.AddScoped<IPoslodavacRepozitorijum, PoslodavacRepozitorijum>();
builder.Services.AddScoped<INezaposleniServis, NenzaposleniServis>();
builder.Services.AddScoped<IPoslodavacServis, PoslodavacServis>();
builder.Services.AddScoped<IRadniOdnosServis, RadniOdnosServis>();
builder.Services.AddScoped<IRadniOdnosRepozitorijum, RadniOdnosRepozitorijum>();
builder.Services.AddScoped<IPoslovnaLogika, PoslovnaLogika>();

// Add services to the container.
builder.Services.AddRazorPages();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
