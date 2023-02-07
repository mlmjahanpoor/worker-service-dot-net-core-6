using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace MyWorker;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddAuthorization();
        services.AddControllers();
        services.AddRouting();

        // worker service config
        services.AddHostedService<Worker>();
        //services.AddSingleton<AppSettings>(
        //    serviceProvider => { return Program.CreateAppSettings(); } // this method could be relocated too
        //);

        // example for health checks
        services.AddHealthChecks();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.Use(async (context, next) =>
        {
            Console.WriteLine("After routing runs, endpoint will be non-null if routing found a match.");
            Console.WriteLine($"Endpoint: {context.GetEndpoint()?.DisplayName ?? "null"}");
            await next(context);
        });
        app.Map("/moslem", d =>
        {
            Console.WriteLine("yes");
        });
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            // example for health checks
            //endpoints.MapHealthChecks("/health", new HealthCheckOptions
            //{
            //    Predicate = _ => true,
            //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            //});
        });
    }
}