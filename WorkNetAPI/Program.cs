using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WorkNet.BLL;
using WorkNet.BLL.Services;
using WorkNet.BLL.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Db Connection 
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.RegisterDbContext(connectionString);

// services from bll
builder.Services.RegisterRepositories();
builder.Services.RegisterServices();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddScoped<IEmployerService, EmployerService>();

// jwt implementation
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
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

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireCandidateRole", policy => policy.RequireRole("Candidate"));
    options.AddPolicy("RequireEmployerRole", policy => policy.RequireRole("Employer"));
});

// log in json format to console
/*builder.Logging.AddJsonConsole(options =>
{
    options.JsonWriterOptions = new()
    {
        Indented = true,
    };
});*/

var app = builder.Build();

app.UseCors(AllowAllpolicy);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

// log http requests
app.UseHttpLogging();

app.MapControllers();

app.Run();
