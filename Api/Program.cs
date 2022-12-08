
using ApplicationCore.Services.PostServices.Command;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("sqlServer")));
builder.Services.AddMediatR(typeof(CreatePostCommand).Assembly);

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
