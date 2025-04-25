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

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Voeg sessie-middleware toe
app.UseAuthorization();

app.MapRazorPages();

app.Run();
