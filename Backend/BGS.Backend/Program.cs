using BGS.Backend;
using BGS.Backend.Helpers;
using BGS.GameAbstractions.Interfaces;
using Catan.Backend.GameManagement;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IGameFactory, CatanGameFactory>();
builder.Services.AddSingleton<IGameManager, GameManager>();
builder.Services.AddSingleton<GameFactoryMapper>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();