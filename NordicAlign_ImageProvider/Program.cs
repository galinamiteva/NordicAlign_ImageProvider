using Azure.Storage.Blobs;
using Data.Contexts;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NordicAlign_ImageProvider.Services;

//var builder = FunctionsApplication.CreateBuilder(args);

//builder.ConfigureFunctionsWebApplication();


//builder.Build().Run();

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddDbContext<DataContext>(x => x.UseSqlServer(Environment.GetEnvironmentVariable("SQLSERVER")));
        services.AddScoped<BlobServiceClient>(x => new BlobServiceClient(Environment.GetEnvironmentVariable("AZURE_STORAGE_ACCOUNT")));
        services.AddScoped<FileService>();
    })
    .Build();

host.Run();
