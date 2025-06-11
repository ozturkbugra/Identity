using Identity.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(x=> 
new SmtpEmailSender (
    builder.Configuration["EmailSender:Host"],
    builder.Configuration.GetValue<int>("EmailSender:Port"),
    builder.Configuration.GetValue<bool>("EmailSender:EnableSSL"),
    builder.Configuration["EmailSender:Username"],
    builder.Configuration["EmailSender:Password"])

);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 6; //Minimum karaktersayý
    options.Password.RequireNonAlphanumeric = false; //Alfanümerik karakter zorunluluðu
    options.Password.RequireLowercase = false; //küçük harf kullanma zorunluluðu
    options.Password.RequireUppercase = false; //büyük harf kullanma zorunluluðu
    options.Password.RequireDigit = false; //rakam kullanma zorunluluðu


    options.User.RequireUniqueEmail = true; //1 mail tek kullanýcý mý

    //options.User.AllowedUserNameCharacters = "abcçdefgðhýijklmnoöprstuüvyzqw"; //bu karakterler dýþýnda tanýmlanamaz

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    // 5 yanlýþ giriþten sonra hesap 5 dakika kitlenir

    options.SignIn.RequireConfirmedEmail = true; // mail onay zorunluluðu
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => { 
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied"; // yetkisiz olduðunda yönlendirileceði kýsým
    options.SlidingExpiration = true; // bu kýsým otomatik süre yeniler alttaki ile 10 gün sonra allta belirtilen kadar
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

IdentitySeedData.IdentityTestUser(app);

app.Run();
