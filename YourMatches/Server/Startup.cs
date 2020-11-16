using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using YourMatches.Server.Services;
using YourMatches.Server.Services.IWebScraper;

namespace YourMatches.Server
{
    public class Startup
    {
        IWebHostEnvironment CurrentEnvironment;

        public Startup(IWebHostEnvironment currentEnvironment)
        {
            CurrentEnvironment = currentEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
            services.AddSingleton(new ApiCallLimiter(10, 60));
            services.AddSingleton<LogoContainer>();
            services.AddHttpClient<WikipediaWebScraper>();
            if (CurrentEnvironment.IsDevelopment())
            {
                services.AddSingleton<IMatchRetriever, MockMatchRetriever>();
            }
            else
            {
                services.AddHttpClient<IMatchRetriever, MatchRetriever>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }

            app.UseStaticFiles();
            app.UseBlazorFrameworkFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
