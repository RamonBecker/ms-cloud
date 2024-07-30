using GeekShopping.Web.Services;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IProductService, ProductService>(
    c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductAPI"])
);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options => {
    options.DefaultScheme = "cookie";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("cookie", c => { c.ExpireTimeSpan = TimeSpan.FromMinutes(10);

        c.Cookie.SameSite = SameSiteMode.None;
        c.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        c.Cookie.IsEssential = true;

    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = builder.Configuration["ServiceUrls:IdentityServer"];
        options.GetClaimsFromUserInfoEndpoint = true;
        options.AuthenticationMethod = OpenIdConnectRedirectBehavior.RedirectGet;
        options.NonceCookie.SecurePolicy = CookieSecurePolicy.Always;
        options.ClientId = "geek_shopping";
        options.ClientSecret = "my_super_secret";
        options.ResponseType = "code";
        options.ClaimActions.MapJsonKey("role", "role", "role");
        options.ClaimActions.MapJsonKey("sub", "sub", "sub");
        options.TokenValidationParameters.NameClaimType = "name";
        options.TokenValidationParameters.RoleClaimType = "role";
        options.Scope.Add("geek_shopping");
        options.SaveTokens = true;
        options.ResponseMode = "query";
        options.UsePkce = true;
        options.ResponseMode = OpenIdConnectResponseMode.FormPost;

    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

