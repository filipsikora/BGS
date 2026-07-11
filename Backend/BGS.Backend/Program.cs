using BGS.Backend;
using BGS.Backend.Helpers;
using BGS.Backend.Interfaces;
using BGS.Backend.Networking;
using BGS.GameAbstractions.Interfaces;
using BGS.Networking.Websockets;
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

app.UseWebSockets();
app.MapControllers();

builder.Services.AddSingleton<SocketManager>();
builder.Services.AddSingleton<ISocketManager, SocketManager>();
builder.Services.AddSingleton<SocketEndpoint>();

app.Map("/ws", async context =>
{
    var endpoint = context.RequestServices.GetRequiredService<SocketEndpoint>();

    await endpoint.Handle(context);
});

app.Run();