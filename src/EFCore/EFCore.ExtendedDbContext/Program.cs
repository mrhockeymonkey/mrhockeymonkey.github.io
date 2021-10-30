using System;
using EFCore.ExtendingDbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MusicDbContext>(options =>
    options
        .LogTo(Console.WriteLine)
        .UseSqlServer(
            "Server=(localdb)\\mssqllocaldb;Database=Music-1;Trusted_Connection=True;MultipleActiveResultSets=true"));
builder.Services.AddScoped<IMusicDbContext>(provider => provider.GetRequiredService<MusicDbContext>());
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "EFCore.ExtendedDbContext", Version = "v1" });
});

var app = builder.Build();

// creates or updates sql db. useful for local development but can result in data loss
// migrations is a better way if data should cannot be lost.
// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<MusicDbContext>();
//     context.Database.EnsureCreated();
// }

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EFCore.ExtendedDbContext v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
