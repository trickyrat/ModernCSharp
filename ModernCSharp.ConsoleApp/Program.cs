using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ModernCSharp.Application;
using ModernCSharp.Application.Services;
using ModernCSharp.ConsoleApp;


var hostBuilder = Host.CreateApplicationBuilder();

hostBuilder.Services.AddSingleton<OrderService>();
hostBuilder.Services.AddFileImporters();
hostBuilder.Services.AddFileExporters();

hostBuilder.Services.AddHostedService<OrderWorker>();


await hostBuilder.Build().RunAsync();