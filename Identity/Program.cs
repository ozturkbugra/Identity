using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<IdentityContext>();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 6; //Minimum karaktersayý
    options.Password.RequireNonAlphanumeric = false; //Alfanümerik karakter zorunluluðu
    options.Password.RequireLowercase = false; //küçük harf kullanma zorunluluðu
    options.Password.RequireUppercase = false; //büyük harf kullanma zorunluluðu
    options.Password.RequireDigit = false; //rakam kullanma zorunluluðu


    options.User.RequireUniqueEmail = true; //1 mail tek kullanýcý mý
    
    //options.User.AllowedUserNameCharacters = "abcçdefgðhýijklmnoöprstuüvyzqw"; //bu karakterler dýþýnda tanýmlanamaz

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

IdentitySeedData.IdentityTestUser(app);

app.Run();
