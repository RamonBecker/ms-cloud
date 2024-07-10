using GeekShopping.Web.Services;
using GeekShopping.Web.Services.IServices;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);



var key = builder.Configuration["ServiceUrls: ProductAPI"];

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<IProductService, ProductService>(
    
    c => c.BaseAddress = new Uri(key)
    
    );


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
