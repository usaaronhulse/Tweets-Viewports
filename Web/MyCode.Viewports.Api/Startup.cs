using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyCode.Viewports.Api.Background;
using MyCode.Viewports.Api.Background.Jobs;
using MyCode.Viewports.Api.Configuration;
using MyCode.Viewports.Api.Data;
using MyCode.Viewports.Api.Hubs;
using MyCode.Viewports.Api.Services;
using MyCode.Viewports.TwitterApi.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace MyCode.Viewports.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Add Swagger.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Viewports.Api", Version = "v1" });
            });

            // Add DbContext. Get connection string from appsettings.json.
            services.AddDbContext<ViewportsDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ViewportsDbContext"));
            });

            // Add the services needed to communicate with Twitter API.
            services.AddTwitterApiServices(Configuration.GetSection("Api").GetSection("TwitterApi"));

            // Set the API configuration from appsettings.json.
            services.Configure<ApiConfiguration>(Configuration.GetSection("Api"));

            // Add any services to DI.
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<ITweetService, TweetService>();
            services.AddScoped<IDashboardTweetService, DashboardTweetService>();

            // Background hosted service.
            services.AddHostedService<TwitterIntegrationBackgroundService>(); // 

            // A singleton service to run jobs.
            services.AddSingleton<ITwitterIntegrationJobService, TwitterIntegrationJobService>();

            // Adds SignalR used for Blazor Wasm application.
            services.AddSignalR();

            // Ensure that CORS is set up against the Blazor Wasm application.
            services.AddCors(setup =>
            {
                // Set up our policy name
                setup.AddPolicy("AllowBlazorWasm", policy =>
                {
                    // Use Api > ClientHosts and get all the clients that will be connecting to this API.
                    policy.WithOrigins(Configuration.GetSection("Api").GetSection("ClientHosts").GetChildren().ToArray().Select(c => c.Value).ToArray()).AllowAnyMethod().AllowAnyHeader().AllowCredentials(); 
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(); // Use Swagger
                app.UseSwaggerUI(options =>
                {
                    // Set Swagger options.
                    options.SwaggerEndpoint("swagger/v1/swagger.json", "ViewportsDbContext.Api v1");
                    options.DocumentTitle = "Viewports";
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection(); // HTTPS redirection.

            app.UseRouting(); // Use routing for MVC.

            app.UseAuthorization();

            app.UseCors("AllowBlazorWasm"); // Allow CORS.

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Add controllers.
                endpoints.MapHub<BlashHub>("/blash-hub"); // Register SignalR hub.
            });
        }
    }
}
