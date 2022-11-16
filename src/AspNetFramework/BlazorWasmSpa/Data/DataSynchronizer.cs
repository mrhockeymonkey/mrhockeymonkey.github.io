using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace BlazorWasmSpa.Data;

public class DataSynchronizer : IDataSynchronizer
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
    private readonly ILogger<DataSynchronizer> _logger;
    private bool _isSyncInProgress;
    
    public event Action<int>? OnProgress;
    public event Action? OnCompleted;

    private Task _initTask;

    public DataSynchronizer(
        IDbContextFactory<ApplicationDbContext> contextFactory,
        ILogger<DataSynchronizer> logger,
        IJSRuntime js)
    {
        _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _initTask = InitializeAsync(js);
    }
    
    public async Task InitializeAsync(IJSRuntime js)
    {
        var module = await js.InvokeAsync<IJSObjectReference>("import", "./dbstorage.js");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("browser")))
        {
            await module.InvokeVoidAsync("synchronizeFileWithIndexedDb", "app.db");
        }

        await using var db = await _contextFactory.CreateDbContextAsync();
        await db.Database.EnsureCreatedAsync(); // Or MigrateAsync()
        _logger.LogInformation("Successfully initialized db in browser");
    }

    public async Task<ApplicationDbContext> GetDbContextAsync()
    {
        await _initTask;
        return await _contextFactory.CreateDbContextAsync();
    }


    public async Task SynchronizeAsync()
    {
        if (_isSyncInProgress) return;
        
        _isSyncInProgress = true;

        await using var context = await GetDbContextAsync();

        var foo = await context.Plants.ToListAsync();
        foreach (var plantEntity in foo)
        {
            _logger.LogInformation(plantEntity.PlantName);
        }
        // context.ChangeTracker.AutoDetectChangesEnabled = false;
        // context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        // Persist a bunch of demo data
        // for (int i = 0; i < 3; i++)
        // {
        //     var entity = new PlantEntity
        //     {
        //         // PlantId = i,
        //         PlantName = $"Plant {i}",
        //         PlantHeightCm = 60d,
        //         LastWatered = DateTimeOffset.Now
        //     };
        //         
        //     // await context.Database.ExecuteSqlRawAsync(
        //     //     "INSERT OR REPLACE INTO Plants (PlantId, PlantName, PlantHeightCm, LastWatered) values ({0}, {1}, {2}, {3})",
        //     //     entity.PlantId,
        //     //     entity.PlantName,
        //     //     entity.PlantHeightCm,
        //     //     entity.LastWatered);
        //
        //     try
        //     {
        //         context.Plants.Add(entity);
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Failed to add plant");
        //     }
        //     
        //
        //     // yields control back to UI thread
        //     // TODO wont be needed when multi threading supported
        //     await Task.Delay(25);
        //     
        //     _logger.LogInformation($"Updated {entity.PlantId}");
        // }

        // try
        // {
        //     context.SaveChanges();
        // }
        // catch (Exception ex)
        // {
        //     _logger.LogError(ex, "Failed to save");
        // }

    }
}