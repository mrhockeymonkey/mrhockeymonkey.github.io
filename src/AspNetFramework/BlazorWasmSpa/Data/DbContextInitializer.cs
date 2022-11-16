using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace BlazorWasmSpa.Data;

public class DbContextInitializer : IDbContextInitializer
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
    private readonly IJSRuntime _js;
    private readonly ILogger<DbContextInitializer> _logger;

    public const string ApplicationDbFileName = "app.db";

    public DbContextInitializer(
        IDbContextFactory<ApplicationDbContext> contextFactory,
        IJSRuntime js,
        ILogger<DbContextInitializer> logger)
    {
        _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        _js = js ?? throw new ArgumentNullException(nameof(js));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        
    }
    
    public async Task InitializeAsync()
    {
        var module = await _js.InvokeAsync<IJSObjectReference>("import", "./dbstorage.js");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("browser")))
        {
            await module.InvokeVoidAsync("synchronizeFileWithIndexedDb", ApplicationDbFileName);
        }

        await using var db = await _contextFactory.CreateDbContextAsync();
        await db.Database.EnsureCreatedAsync(); // Or MigrateAsync()
        _logger.LogInformation("Successfully initialized db in browser");
    }
}