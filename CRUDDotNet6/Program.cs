using CRUDDotNet6.Models;
using CRUDDotNet6.Repositories;
using CRUDDotNet6.Services;
using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using CRUDDotNet6.Utils;
using Azure.Extensions.AspNetCore.Configuration.Secrets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var defaultCredentials = new DefaultAzureCredential();
var keyVaultEndpoint = builder.Configuration["AzureKeyVaultEndpoint"];

//builder.AddAzureKeyVault(new Uri(keyVaultEndpoint));
builder.Configuration.AddAzureKeyVault(new Uri(keyVaultEndpoint), defaultCredentials,
    new AzureKeyVaultConfigurationOptions
    {
        // Manager = new PrefixKeyVaultSecretManager(secretPrefix),
        ReloadInterval = TimeSpan.FromMinutes(5)
    });

builder.Services.AddControllers();
/// configure strongly typed settings object
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
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
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Student Details",
        Description = "REST API for showing student details",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Pratap",
            Email = "skondreddy@presidio.com",
            Url = new Uri("https://twitter.com/saipratap1012"),
        },
        License = new OpenApiLicense
        {
            Name = "License Information",
            Url = new Uri("https://example.com/license"),
        }
    });



    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
    
});


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();


app.UseAuthorization();

app.MapControllers();

app.Run();

