using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MinimalWarhammerRest.Domain;
using MinimalWarhammerRest.Endpoints;
using MinimalWarhammerRest.Factions;
using MinimalWarhammerRest.Miniatures;
using MinimalWarhammerRest.Models;
using MinimalWarhammerRest.Services.TimeService;
using MinimalWarhammerRest.StructuredLogging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<WarhammerDBContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddMemoryCache();
builder.Services.AddScoped<FactionService>();
builder.Services.AddScoped<IFactionService>(x => new CachedFactionService(x.GetRequiredService<FactionService>(), x.GetRequiredService<IMemoryCache>()));
builder.Services.AddScoped<MiniatureService>();
builder.Services.AddScoped<IMiniatureService>(x => new CachedMiniatureService(x.GetRequiredService<MiniatureService>(), x.GetRequiredService<IMemoryCache>()));
builder.Services.AddSingleton<TimeHelper>();
builder.Services.AddScoped<ITimeService>(p => new TimeFreeze(p.GetRequiredService<TimeHelper>()));

builder.Services.AddScoped<IResponseFactory, ReponseFactory>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapFactionEndpoints();
app.MapMiniatureEndpoints();

app.UseStructuredLogging();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(op =>
    {
        op.DisplayRequestDuration();
    });
}

app.Run();