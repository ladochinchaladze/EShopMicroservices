

using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

//Add Services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

//Configure the Http Request pipeline.
app.MapCarter();

app.Run();
