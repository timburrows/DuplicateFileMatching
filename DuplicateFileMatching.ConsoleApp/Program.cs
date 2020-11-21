using System;
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
            scope.ServiceProvider.GetRequiredService<IAppHost>().Run();
            
            Dispose();
        }

        private static void Init()
        {
            var services = new ServiceCollection()
                .AddSingleton<IAppHost, AppHost>();
            
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