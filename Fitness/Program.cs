using FitnessDAL.Repositories;
using FitnessCore.Services;
using FitnessCore.Repositories;
using FitnessCore;
using FitnessDAL.Interfaces;
using FitnessCore.Interfaces;

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

// Registreer IUserRepository met een connection string
builder.Services.AddScoped<IUserRepository>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new UserRepository(connectionString);
});

// Registreer IUserService
builder.Services.AddScoped<IUserService, UserService>();

// Registreer ITrainerRepository met een connection string
builder.Services.AddScoped<ITrainerRepository>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new TrainerRepository(connectionString);
});

// Registreer ITrainerService
builder.Services.AddScoped<ITrainerService, TrainerService>();

// Registreer IMealLogRepository met een connection string
builder.Services.AddScoped<IMealLogRepository>(provider =>
    new MealLogRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registreer IMealRepository met een connection string
builder.Services.AddScoped<IMealRepository>(provider =>
    new MealRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registreer IMealService
builder.Services.AddScoped<IMealService, MealService>();

// Registreer MealLogService
builder.Services.AddScoped<MealLogService>(provider =>
    new MealLogService(
        provider.GetRequiredService<IMealLogRepository>(),
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Voeg sessie-middleware toe
app.UseAuthorization();

app.MapRazorPages();

app.Run();
