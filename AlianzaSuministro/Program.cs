using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AlianzaSuministro.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AlianzaSuministroContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AlianzaSuministroContext") ?? throw new InvalidOperationException("Connection string 'AlianzaSuministroContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });
builder.Services.Configure<IISServerOptions>(options =>
{
    options.AutomaticAuthentication = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Producto}/{action=Index}/{id?}");

app.Run();
