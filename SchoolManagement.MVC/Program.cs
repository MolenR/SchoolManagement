using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.MVC.Data;
using Serilog;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the IoC container.

/* Dependency Registration Injection 
----------------------------------*/
var connectionString = 
    builder.Configuration
    .GetConnectionString("SchoolManagementDbConnection") ?? throw new InvalidOperationException("Connection string 'SchoolManagementDb' not found.");
builder.Services.AddDbContext<SchoolManagementDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<SchoolManagementDbContext>();

/* Configuration of Auth0 Authentication services 
-----------------------------------------------*/
builder.Services
        .AddAuth0WebAppAuthentication(options => {
            options.Domain = builder.Configuration["Auth0:Domain"];
            options.ClientId = builder.Configuration["Auth0:ClientId"];
        });

/* CONFIGURE SERILOG 
------------------*/
builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));


/* CONNECTION WITH COSMOSDB*/
var documentClient = new CosmosClient(builder.Configuration
    .GetConnectionString("CosmosConnection"));
builder.Services.AddSingleton(documentClient);

/* ADDING NOTYF 
-------------*/
builder.Services.AddNotyf(c =>
{
    c.DurationInSeconds = 5;
    c.IsDismissable = true;
    c.Position = NotyfPosition.TopRight;
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

/* INVOKE SERILOG 
---------------*/
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

/*-----IMPORTANT----- */
/* Auth0 Authentication => Authorization */
// Looks for the user after user is ok then give access
app.UseAuthentication();
app.UseAuthorization();

app.UseNotyf();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
