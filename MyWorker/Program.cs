using Microsoft.AspNetCore.Hosting;
using MyWorker;
using Serilog;
using System.Reflection.PortableExecutable;

IHost host = Host.CreateDefaultBuilder(args)
    .UseSerilog()
    .UseWindowsService()
    .ConfigureServices((hostContext, services) =>
    {
        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(hostContext.Configuration).CreateLogger();
        //services.AddHostedService<Worker>();
    }).ConfigureWebHostDefaults(b =>
    {
        b.UseUrls("http://*:1200");
        b.UseStartup<Startup>();
    })
    .Build();
await host.RunAsync();