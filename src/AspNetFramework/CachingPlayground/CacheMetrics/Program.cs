using CacheMetrics;
using CacheMetrics.BackgroundServices;
using CacheMetrics.Services;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache(options => options.TrackStatistics = true);

// asp.net "native" decoration
// builder.Services
//     .AddTransient<CoffeeProvider>()
//     .AddTransient<ICoffeeProvider>(provider => new CoffeeProviderCache(
//         provider.GetRequiredService<CoffeeProvider>(), 
//         new InstrumentedMemoryCache<CoffeeProviderCache>(provider.GetRequiredService<IMemoryCache>())));

// Scrutor decoration
builder.Services
    .AddSingleton<IMemoryCache<CoffeeProviderCache>, InstrumentedMemoryCache<CoffeeProviderCache>>()
    .AddTransient<ICoffeeProvider, CoffeeProvider>()
        .Decorate<ICoffeeProvider, CoffeeProviderCache>();

builder.Services.AddHostedService<PrometheusBeforeCollectionBootstrap>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapMetrics();

app.Run();