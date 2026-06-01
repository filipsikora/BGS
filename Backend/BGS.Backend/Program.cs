using BGS.Backend;
using BGS.Backend.Helpers;
using BGS.Backend.Interfaces;
using BGS.GameAbstractions.Interfaces;
using BGS.Persistence;
using BGS.Persistence.Context;
using Catan.Backend.GameManagement;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IGameFactory, CatanGameFactory>();
builder.Services.AddSingleton<IGameManager, GameManager>();
builder.Services.AddSingleton<IGameFactoryMapper, GameFactoryMapper>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
});

builder.Services.AddDbContext<BgsDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddScoped<IGameRepository, EfGameRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();