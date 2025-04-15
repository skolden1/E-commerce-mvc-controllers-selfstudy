using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CTRLE_Handel_oev_identity.Areas.Identity.Data;
using CTRLE_Handel_oev_identity.Datas;

var builder = WebApplication.CreateBuilder(args);

// Hämta connection string för IdentityDbContext
var connectionString = builder.Configuration.GetConnectionString("IdentityDbContextConnection")
    ?? throw new InvalidOperationException("Connection string 'IdentityDbContextConnection' not found.");

// Lägg till IdentityDbContext
builder.Services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(connectionString));

// Lägg till Identity och använd den med Entity Framework
builder.Services.AddDefaultIdentity<IdentityUserTable>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<IdentityDbContext>();

// Lägg till AppDbContext för din andra databas
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Lägg till Razor Pages
builder.Services.AddRazorPages();

// Lägg till controllers och views
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

// Använd statiska filer för t.ex. bilder, CSS, JS
app.UseStaticFiles();

app.UseAuthorization();

// Mappar Razor Pages
app.MapRazorPages();  // Endast denna behövs för Razor Pages

// Mappar standardrutt för MVC (controllers)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();