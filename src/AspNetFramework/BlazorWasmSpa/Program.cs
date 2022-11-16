using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWasmSpa;
using BlazorWasmSpa.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddDbContextFactory<ApplicationDbContext>(options => 
    options.UseSqlite($"Filename={DbContextInitializer.ApplicationDbFileName}"));


// builder.Services.AddSingleton<IDbContextInitializer, DbContextInitializer>();
builder.Services.AddScoped<IDataSynchronizer, DataSynchronizer>();

builder.Services.AddQuickGridEntityFrameworkAdapter();

var app = builder.Build();

// await app.Services.GetRequiredService<IDbContextInitializer>().InitializeAsync();

await app.RunAsync();
