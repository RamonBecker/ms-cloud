using RestWithASPNETVS.Services;
using RestWithASPNETVS.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


// Injeção de depend~encia
builder.Services.AddScoped<IPersonService, PersonServiceImpletation>();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
