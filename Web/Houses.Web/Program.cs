using System.Globalization;
using Houses.Infrastructure.Data;
using Houses.Infrastructure.Data.Identity;
using Houses.Web.Extensions;
using Houses.Web.ModelBinders;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using static Houses.Infrastructure.Constants.ValidationConstants.FormattingConstant;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationDbContexts(builder.Configuration);

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddApplicationServices();

//builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedLanguages = new[]
    {
        new CultureInfo("bg"),
        new CultureInfo("en")
    };

    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedLanguages;
    options.SupportedUICultures = supportedLanguages;
});

builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
        options.ModelBinderProviders.Insert(1, new DateTimeModelBinderProvider(NormalDateFormat));
    })
    .AddMvcLocalization(LanguageViewLocationExpanderFormat.Suffix);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();
app.UseRequestLocalization();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
