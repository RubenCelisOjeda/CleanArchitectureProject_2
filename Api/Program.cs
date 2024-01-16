using Application;
using Application.Auth.Tokens;
using Application.Auth.Users;
using Application.Common.Interfaces;
using Application.Core.Organizations;
using Application.Core.Products;
using Infrastructure;
using Infrastructure.Common.Services;
using Infrastructure.Core;
using Infrastructure.Persistence.Context;
using Microsoft.OpenApi.Models;
using MultiTenant.Infrastructure.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bear", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Be",
        BearerFormat = "JWT"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "B"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IOrganizationService, OrganizationService>();
builder.Services.AddTransient<ISerializerService, NewtonSoftService>();
builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddTransient<MasterDbContext>();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

await app.Services.InitializeDatabasesAsync();

app.UseInfrastructure(builder.Configuration);

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
