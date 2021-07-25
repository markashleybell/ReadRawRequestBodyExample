using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ReadRawRequestBodyExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddControllersWithViews();
        }

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

            But encapsulating it in its own class is better:

            app.UseMiddleware<EnableRequestBodyBufferingMiddleware>();

            And even better: only applying the middleware to the routes that
            actually need it:
            */

            app.UseWhen(
                ctx => ctx.Request.Path.StartsWithSegments("/home/withmodelbinding"),
                ab => ab.UseMiddleware<EnableRequestBodyBufferingMiddleware>()
            );

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}
