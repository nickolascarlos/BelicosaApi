using AutoMapper;
using BelicosaApi;
using BelicosaApi.AuthorizationHandlers;
using BelicosaApi.AutoMapper;
using BelicosaApi.BusinessLogic;
using BelicosaApi.DTOs;
using BelicosaApi.Enums;
using BelicosaApi.Models;
using BelicosaApi.ModelsServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BelicosaApiContext>();
builder.Services.AddAutoMapper(t =>
{
    t.AddProfile(typeof(MapperConfig));
});
builder.Services.AddAuthentication();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(CustomPolicies.UserIsGameOwner, policy =>
    {
        policy.Requirements.Add(new UserIsGameOwnerRequirement());
    });
});

builder.Services.AddScoped<BelicosaGameService>();
builder.Services.AddScoped<PlayerService>();
builder.Services.AddScoped<TerritoryService>();
builder.Services.AddScoped<ContinentService>();
//builder.Services.AddScoped<PlayerService>();



builder.Services.AddSingleton<IAuthorizationHandler, UserIsGameOwnerAuthorizationHandler>();
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                .AddEntityFrameworkStores<BelicosaApiContext>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<IdentityUser>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
