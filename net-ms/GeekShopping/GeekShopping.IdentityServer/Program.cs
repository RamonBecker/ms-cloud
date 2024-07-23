using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Model;
using GeekShopping.IdentityServer.Model.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();


var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<MySQLContext>().AddDefaultTokenProviders();

builder.Services.AddIdentityServer(options =>
                                    {
                                        options.Events.RaiseErrorEvents = true;
                                        options.Events.RaiseInformationEvents = true;
                                        options.Events.RaiseFailureEvents = true;
                                        options.Events.RaiseSuccessEvents = true;
                                        options.EmitStaticAudienceClaim = true;
                                    }
                                  ).AddInMemoryIdentityResources(
                                                                 IdentityConfiguration.IdentityResources)
                                   .AddInMemoryClients(IdentityConfiguration.Clients)
                                   .AddAspNetIdentity<ApplicationUser>();
                                  


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
