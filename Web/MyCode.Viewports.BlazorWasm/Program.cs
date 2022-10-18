using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using MyCode.Viewports.BlazorWasm.Services.Api;
using MyCode.Viewports.BlazorWasm.Services.Dashboard;
using MyCode.Viewports.BlazorWasm.Services.Loader;
using MyCode.Viewports.BlazorWasm.Services.Modal;

namespace MyCode.Viewports.BlazorWasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app"); // Attach the Blazor Wasm app to an HTML element with an ID of "app"

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // Create new services in dependency injection.
            builder.Services.AddScoped<IModalService, ModalService>();
            builder.Services.AddScoped<ILoaderService, LoaderService>();
            builder.Services.AddScoped<IDashboardService, DashboardService>();
            builder.Services.AddScoped<IApiService, ApiService>();

            await builder.Build().RunAsync();
        }
    }
}
