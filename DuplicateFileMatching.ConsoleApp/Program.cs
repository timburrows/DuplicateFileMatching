using System;
using DuplicateFileMatching.Core;
using Microsoft.Extensions.DependencyInjection;

namespace DuplicateFileMatching.ConsoleApp
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        
        public static void Main(string[] args)
        {
            Init();
            
            var scope = _serviceProvider.CreateScope();
            scope.ServiceProvider.GetRequiredService<IAppHost>().Run(args);
            
            Dispose();
        }

        private static void Init()
        {
            var services = new ServiceCollection()
                .AddSingleton<IAppHost, AppHost>()
                .AddTransient<IBitmapManipulation, BitmapManipulation>()
                .AddTransient<IBitmapComparison, BitmapComparison>()
                .AddTransient<IFileService, FileService>();
            
            _serviceProvider = services.BuildServiceProvider(true);
        }

        private static void Dispose()
        {
            if (_serviceProvider == null) return;
            
            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}