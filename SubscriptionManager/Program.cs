using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SubscriptionManager.Context;
using SubscriptionManager.Services;
using SubscriptionManager.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// if (builder.Environment.IsDevelopment())
// {
//     // Use in-memory database for testing
//     builder.Services.AddDbContext<BiddingSystemContext>(options =>
//         options.UseInMemoryDatabase("TestDatabase"));
// }
// else
// {
builder.Services.AddDbContext<SubscriptionManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("subscription-manager-context") ??
                         throw new InvalidOperationException(
                             "Connection string 'subscription-manager-context' not found.")));
// }

builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

//Works for users having multiple roles. looks through all of them and checks if the user has the required one
builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("User", policy =>
            policy.RequireAssertion(context =>
                context.User.HasClaim(claim => claim is { Type: ClaimTypes.Role, Value: "User" })
            )
        );
        options.AddPolicy("Admin", policy =>
            policy.RequireAssertion(context =>
                context.User.HasClaim(claim => claim is { Type: ClaimTypes.Role, Value: "Admin" })
            )
        );
        options.AddPolicy("UserOrAdmin", policy =>
            policy.RequireAssertion(context =>
                context.User.HasClaim(claim =>
                    claim is { Type: ClaimTypes.Role, Value: "User" }
                        or { Type: ClaimTypes.Role, Value: "Admin" }
                )
            )
        );
    }
);

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
        ValidIssuer = "SubscriptionManager",
        ValidAudience = "http://localhost:5185",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration.GetSection("JwtSettings:Token").Value!))
    };
});

builder.Services.AddControllers();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // Optional: Set the session timeout period; 20 minutes is the default.
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true; // Helps prevent JavaScript access to the session cookie.
    options.Cookie.IsEssential = true; // Allows session cookie even if the user hasn't consented to cookies.
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Subscription Manager", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();