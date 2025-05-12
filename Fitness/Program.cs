using FitnessDAL.Repositories;
using FitnessCore.Services;
using FitnessCore.Repositories;
using FitnessCore.Service;

var builder = WebApplication.CreateBuilder(args);

// Voeg sessieondersteuning toe
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Stel de sessietijd in
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Voeg Razor Pages ondersteuning toe
builder.Services.AddRazorPages();

// Registreer UserRepository met een connection string
builder.Services.AddScoped<UserRepository>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new UserRepository(connectionString);
});

// Registreer UserService
builder.Services.AddScoped<UserService>();

// Registreer TrainerRepository met een connection string
builder.Services.AddScoped<TrainerRepository>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new TrainerRepository(connectionString);
});

// Registreer TrainerService
builder.Services.AddScoped<TrainerService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Voeg sessie-middleware toe
app.UseAuthorization();

app.MapRazorPages();

app.Run();
