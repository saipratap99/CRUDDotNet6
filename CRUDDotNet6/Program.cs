using CRUDDotNet6.Models;
using CRUDDotNet6.Repositories;
using CRUDDotNet6.Services;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var defaultCredentials = new DefaultAzureCredential();

/*
var settings = builder.Build();
var keyVaultEndpoint = settings["AzureKeyVaultEndpoint"];


builder.AddAzureKeyVault((keyVaultEndpoint, defaultCredentials,
    new AzureKeyVaultConfigurationOptions
    {
        // Manager = new PrefixKeyVaultSecretManager(secretPrefix),
        ReloadInterval = TimeSpan.FromMinutes(5)
    });

*/
builder.Services.AddControllers();

builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IStudentService, StudentService>();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IStudentRepository, StudentRepository>();
// DB Config
builder.Services.AddDbContext<studentsDBContext>(options =>
{
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));
    options.UseMySql(builder.Configuration.GetConnectionString("DotNetTrainingDB"), serverVersion);
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

