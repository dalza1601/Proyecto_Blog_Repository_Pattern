using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto_Blog.BE.Models;
using Proyecto_Blog.DL.DA.Data;
using Proyecto_Blog.DL.DA.Data.Repository;
using Proyecto_Blog.DL.DA.Data.Repository.IRepository;
using Proyecto_Blog.DL.DA.Seed;
using Proyecto_Blog.UI.ASPNET.MVC.Helppers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("SQLSERVER_CONECTION") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI();


builder.Services.AddControllersWithViews();

//Agregar al orquestador de trabajo a traves de inyeccion de dependencias.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// después de builder creado
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new MappingHelpper());
});

var app = builder.Build();

SeedData.Initialize(app.Services); // Ejecutar el seeding de datos al iniciar la aplicación

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Client}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
