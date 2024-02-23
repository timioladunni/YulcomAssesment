using AspNetCoreRateLimit;
using DikriptVerify.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SecurityLibrary.Cryptography;
using System.Reflection;
using System.Text;
using YulcomAssesment.API.Configurations;
using YulcomAssesment.API.Constants;
using YulcomAssesment.API.Data;
using YulcomAssesment.API.Services;

var builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration = builder.Configuration;
IWebHostEnvironment env = builder.Environment;
string corsPolicyName = ProgramConstants.CORS_POLICY;

YulcomAssesmentConfiguration configuration = new YulcomAssesmentConfiguration();
Configuration.GetSection("YulcomAssesmentConfiguration").Bind(configuration);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton(configuration);

//If you are passing an unencrpted connection string, The decrypt Method has to be removed

builder.Services
    .AddDbContextPool<YulcomAssesmentContext>(o => o.UseSqlServer(EncryptionUtility
    .Decrypt(Configuration.GetConnectionString("YulcomConString")), 
    options => options.EnableRetryOnFailure(maxRetryCount: 100, maxRetryDelay: TimeSpan.FromSeconds(30),
    errorNumbersToAdd: null)));

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "YulcomAssement.Api", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName,
        builder =>
        {
            builder.WithOrigins(configuration.AllowedOrigins);
        });
});

builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.EnableEndpointRateLimiting = true;
    options.StackBlockedRequests = false;
    options.HttpStatusCode = 429;
    options.RealIpHeader = "X-Real-IP";
    options.ClientIdHeader = "X-ClientId";
    options.GeneralRules = new List<RateLimitRule>
    {
        new RateLimitRule
        {
            Endpoint = "/api/v1/TransferToBank",
            Period = "10s",
            Limit = 1,
        }
    };
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddInMemoryRateLimiting();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

//Authentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(EncryptionUtility.Decrypt(configuration.EncryptionKey))),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Inject Services 
builder.Services.InjectDependencies(Configuration);


var app = builder.Build();
ILoggerFactory loggerFactory = app.Services.GetService<ILoggerFactory>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Yulcom.Assement.Api v1"));
}

loggerFactory.AddFile("C:/Logs/YulcomAssement-{Date}.txt");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseIpRateLimiting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

using (var scope = app.Services.CreateScope())
{
    app.SeedData(configuration);
}

app.Run();
