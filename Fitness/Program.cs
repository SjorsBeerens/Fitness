using Fitness.Repositories;

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

// Registreer TrainerRepository met een connection string
builder.Services.AddScoped<TrainerRepository>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new TrainerRepository(connectionString);
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Voeg sessie-middleware toe
app.UseAuthorization();

app.MapRazorPages();

app.Run();
