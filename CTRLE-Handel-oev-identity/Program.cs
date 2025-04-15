using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CTRLE_Handel_oev_identity.Areas.Identity.Data;
using CTRLE_Handel_oev_identity.Datas;

var builder = WebApplication.CreateBuilder(args);

// H�mta connection string f�r IdentityDbContext
var connectionString = builder.Configuration.GetConnectionString("IdentityDbContextConnection")
    ?? throw new InvalidOperationException("Connection string 'IdentityDbContextConnection' not found.");

// L�gg till IdentityDbContext
builder.Services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(connectionString));

// L�gg till Identity och anv�nd den med Entity Framework
builder.Services.AddDefaultIdentity<IdentityUserTable>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<IdentityDbContext>();

// L�gg till AppDbContext f�r din andra databas
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// L�gg till Razor Pages
builder.Services.AddRazorPages();

// L�gg till controllers och views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurera HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Anv�nd statiska filer f�r t.ex. bilder, CSS, JS
app.UseStaticFiles();

app.UseAuthorization();

// Mappar Razor Pages
app.MapRazorPages();  // Endast denna beh�vs f�r Razor Pages

// Mappar standardrutt f�r MVC (controllers)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();