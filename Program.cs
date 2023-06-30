using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;
using WikidVueServer.DataAccess;
using WikidVueServer.DataAccess.Abstractions;
using WikidVueServer.Middleware;
using WikidVueServer.Services;
using WikidVueServer.Services.Abstractions;

namespace WikidVueServer;

public class Program
{
    #region Fields

    private static readonly IConfigurationRoot _configuration;
    private static readonly AppSettings _appSettings;

    #endregion

    #region Constructor

    static Program()
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .Build();
        _appSettings = new();
        _configuration.Bind(_appSettings);
    }

    #endregion

    #region Public Methods

    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        BuildServices(builder.Services);

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {

        }

        app.UseMiddleware<AuthMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    #endregion

    #region Private Methods

    private static void BuildServices(IServiceCollection services)
    {
        services.AddSingleton(_appSettings);

        services.AddDbContextPool<UserDbContext>(options => options.UseSqlite(_appSettings.DatabaseConnection));
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserRepository, UserRepository>();

        IAsyncPolicy<HttpResponseMessage> retryPolicy = HttpPolicyExtensions
           .HandleTransientHttpError()
           .OrResult(response => response.StatusCode == HttpStatusCode.NotFound)
           .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        services.AddHttpClient(_appSettings.HttpClientName)
            .AddPolicyHandler(retryPolicy);

        services.AddControllers();
    }

    #endregion
}