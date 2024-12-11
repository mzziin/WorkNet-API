using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WorkNet.BLL;

var builder = WebApplication.CreateBuilder(args);


// Cors settings
var AllowAllpolicy = "AllowAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowAllpolicy,
    policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

// httplogging
builder.Services.AddHttpLogging(logging =>
{
    // Not logging body content to prevent exposure of sensitive information (e.g., passwords, personal data)
    logging.LoggingFields = HttpLoggingFields.RequestHeaders | HttpLoggingFields.ResponseHeaders;
    logging.RequestBodyLogLimit = 4096; // Set limit for request body
    logging.ResponseBodyLogLimit = 4096; // Set limit for response body
});

// swagger
builder.Services.AddEndpointsApiExplorer();
// set swagger for jwt
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Enter 'Bearer [jwt]'",
        Type = SecuritySchemeType.ApiKey
    });

    var scheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    options.AddSecurityRequirement(new OpenApiSecurityRequirement { { scheme, Array.Empty<string>() } });
});

// Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Db Connection 
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
// for docker container
string DockerconnectionString = $"Data Source={dbHost}; Initial Catalog={dbName}; User ID=sa; Password={dbPassword}; TrustServerCertificate=True";
// for localhost
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.RegisterDbContext(DockerconnectionString);

// services from bll
builder.Services.RegisterRepositories();
builder.Services.RegisterServices();

builder.Services.AddHttpContextAccessor();

// jwt implementation
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

// log in json format to console
/*builder.Logging.AddJsonConsole(options =>
{
    options.JsonWriterOptions = new()
    {
        Indented = true,
    };
});*/

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

app.UseCors(AllowAllpolicy);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(_ => { });

app.UseAuthentication();
app.UseAuthorization();

// log http requests
app.UseHttpLogging();

app.MapControllers();

app.Run();