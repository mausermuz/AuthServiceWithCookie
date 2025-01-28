using AuthServiceBulgakov.Api.Extensions;
using AuthServiceBulgakov.Api.Filters;
using AuthServiceBulgakov.Api.Helpers;
using AuthServiceBulgakov.Api.Middlewares;
using AuthServiceBulgakov.Application;
using AuthServiceBulgakov.Application.Options;
using AuthServiceBulgakov.DataAccess.MSSQL;
using AuthServiceBulgakov.Infrastructure.Impl;
using AuthServiceBulgakov.Infrastructure.Impl.Authenticated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(options => builder.Configuration.GetSection("JwtSettings").Bind(options));

//Custom services
builder.Services.AddMsSQL(builder.Configuration)
                .AddInfrastructure()
                .AddApplication()
                .AddCustomAuthentication(builder.Configuration);

builder.Services.AddScoped<DbIniailize>();
builder.Services.AddSingleton<IAuthorizationHandler, RolesAuthorizeHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, RolesAuthorizePolicyProvider>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthServiceExceptionFilter());
}).AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomCors();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var dDbInitializer = scope.ServiceProvider.GetRequiredService<DbIniailize>();
    await dDbInitializer.InitializeDatabase();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAllRequestFromlocalhost4200");

app.UseMiddleware<CheckIsActiveUserMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }