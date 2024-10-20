using Wallets.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
//builder.Services.AddApplicationDependency();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();   

app.Run();
