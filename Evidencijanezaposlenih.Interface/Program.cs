using Evidencijanezaposlenih.Interface.Context;
using EvidencijaNezaposlenih.PoslovnaLogika.Interfejsi;
using EvidencijaNezaposlenih.PoslovnaLogika.Validacija;
using EvidencijaNezaposlenih.Repozitorijum.Context;
using EvidencijaNezaposlenih.Repozitorijum.Interfejsi;
using EvidencijaNezaposlenih.Repozitorijum.Repozitorijumi;
using EvidencijaNezaposlenih.Servisi.Interfejsi;
using EvidencijaNezaposlenih.Servisi.Servisi;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Evidencijanezaposlenih.Interface.Context.Modeli;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDbContext<EvidencijaNezaposlenihDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EvidencijaNezaposlenihDBContext"))
           .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddDbContext<IdentitetiDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentitetiDBContext"))
           .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddDefaultIdentity<Korisnik>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentitetiDBContext>();

builder.Services.AddScoped<INezaposleniRepozitorijum, NezaposleniRepozitorujum>();
builder.Services.AddScoped<IPoslodavacRepozitorijum, PoslodavacRepozitorijum>();
builder.Services.AddScoped<INezaposleniServis, NenzaposleniServis>();
builder.Services.AddScoped<IPoslodavacServis, PoslodavacServis>();
builder.Services.AddScoped<IRadniOdnosServis, RadniOdnosServis>();
builder.Services.AddScoped<IRadniOdnosRepozitorijum, RadniOdnosRepozitorijum>();
builder.Services.AddScoped<IPoslovnaLogika, PoslovnaLogika>();
builder.Services.AddScoped<IRadUStruci, RadUStruci>();

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "CashRegister.API", Version = "v1" });
});

builder.Services.AddRazorPages();
builder.Services.AddHttpClient();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var dbContext = services.GetRequiredService<EvidencijaNezaposlenihDBContext>();
        var identityContext = services.GetRequiredService<IdentitetiDBContext>();

        dbContext.Database.Migrate();
        identityContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();

    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
    // specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "swagger"; // To serve Swagger UI at the app's root
    });
}
else
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
app.MapControllers();


app.Run();
