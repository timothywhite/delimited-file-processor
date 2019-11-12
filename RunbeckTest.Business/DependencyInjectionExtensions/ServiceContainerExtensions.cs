using CsvHelper;
using Microsoft.Extensions.DependencyInjection;
using RunbeckTest.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Text;

namespace RunbeckTest.Business.DependencyInjectionExtensions
{
    public static class ServiceContainerExtensions
    {
        public static IServiceCollection AddDilimitedFileService(this IServiceCollection services)
        {
            services.AddSingleton<IResultPublishService>(s => new FileSystemResultPublishService(s.GetRequiredService<IFileSystem>(), "./results", "./errors"));
            services.AddSingleton<IDelimitedFileService, DelimitedFileService>();
            services.AddSingleton<IFileOptionsService, ConsoleFileOptionsService>();
            services.AddScoped<IFileSystem, FileSystem>();
            services.AddScoped<IFactory, Factory>();

            return services;
        }
    }
}
