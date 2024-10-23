using Asp.Versioning;

using Wallets.Api.Commons;
using Wallets.Application;
using Wallets.Database;

var builder = WebApplication.CreateBuilder(args);

var configuration = Configuration.GetConfiguration();
builder.Services.AddSingleton(configuration);

builder.Services.AddApplicationDependency();
builder.Services.AddDatabaseDependency(configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
{
     options.DefaultApiVersion = new ApiVersion(1);
     options.ReportApiVersions = true;
     options.AssumeDefaultVersionWhenUnspecified = true;
})
.AddMvc()
.AddApiExplorer(options =>
{
     options.GroupNameFormat = "'v'V";
     options.SubstituteApiVersionInUrl = true;
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
