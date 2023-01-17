using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.MVC.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the IoC container.

/* Dependency Registration Injection */
var connectionString = 
    builder.Configuration
    .GetConnectionString("SchoolManagementDbConnection") ?? throw new InvalidOperationException("Connection string 'SchoolManagementDb' not found.");
builder.Services.AddDbContext<SchoolManagementDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<SchoolManagementDbContext>();

/* Configuration of Auth0 Authentication services */
builder.Services
        .AddAuth0WebAppAuthentication(options => {
            options.Domain = builder.Configuration["Auth0:Domain"];
            options.ClientId = builder.Configuration["Auth0:ClientId"];
        });

builder.Services.AddControllersWithViews();

var app = builder.Build();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
