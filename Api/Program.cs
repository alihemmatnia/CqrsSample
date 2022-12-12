
using ApplicationCore.Services.Behaviors;
using ApplicationCore.Services.PostServices.Command;
using Infrastructure;
using Infrastructure.RedisDb;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("sqlServer")));
builder.Services.AddMediatR(typeof(CreatePostCommand).Assembly);
builder.Services.AddSingleton<IRedisManager, RedisManager>();
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(EventLoggerBehavior<,>));
builder.Services.AddSingleton<IConnectionMultiplexer>(provider => ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("redis")));
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("redis");
    options.InstanceName = "";
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
