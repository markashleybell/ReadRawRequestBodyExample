using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ReadRawRequestBodyExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) =>
            services.AddControllersWithViews();

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            /*
            We could just inline the middleware to enable buffering here, e.g.

            app.Use(next => context => {
                context.Request.EnableBuffering();
                return next(context);
            });

            But encapsulating it in its own class is better.
            */
            app.UseMiddleware<EnableRequestBodyBufferingMiddleware>();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}
