using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using YourMatches.Client.Services;

namespace YourMatches.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            _ = builder.Services.AddSingleton<LogoDtoContainer>();
            await builder.Build().RunAsync();
        }
    }
}
