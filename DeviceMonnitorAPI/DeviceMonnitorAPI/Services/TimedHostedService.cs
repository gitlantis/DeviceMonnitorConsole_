using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceMonnitorAPI.Services
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<TimedHostedService> _logger;        
        private Timer _timer;
        private readonly IServiceScopeFactory _scopeFactory;
        
        private readonly IFirebaseConfig fbc = new FirebaseConfig()
        {
            AuthSecret = Constants.FirebaseSecret,
            BasePath = Constants.FirebaseUrl
        };
        private IFirebaseClient client;

        public TimedHostedService(ILogger<TimedHostedService> logger, IServiceScopeFactory scopeFactory)
        {            
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {            
            client = new FirebaseClient(fbc);
            client.Delete($"{Constants.baseTree}");
            _logger.LogInformation("Timed Hosted Service running.");

            DoWork(stoppingToken);
            return Task.CompletedTask;
        }

        private async void DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Task t = Task.Factory.StartNew(() => {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var dataService = scope.ServiceProvider.GetRequiredService<DeviceDataService>();                        
                        //dataService.AddData();
                    }
                });

                var count = Interlocked.Increment(ref executionCount);
                _logger.LogInformation(
                    "Timed Hosted Service is working. Count: {Count}", count);

                t.Wait();
            }
        }
        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }    
    
    
    
    
    
    
    
    
    //public class TimedHostedService : IHostedService, IDisposable
    //{
    //    private int executionCount = 0;
    //    private readonly ILogger<TimedHostedService> _logger;        
    //    private Timer _timer;
    //    private readonly IServiceScopeFactory _scopeFactory;
        
    //    private readonly IFirebaseConfig fbc = new FirebaseConfig()
    //    {
    //        AuthSecret = Constants.FirebaseSecret,
    //        BasePath = Constants.FirebaseUrl
    //    };
    //    private IFirebaseClient client;

    //    public TimedHostedService(ILogger<TimedHostedService> logger, IServiceScopeFactory scopeFactory)
    //    {            
    //        _logger = logger;
    //        _scopeFactory = scopeFactory;
    //    }

    //    public Task StartAsync(CancellationToken stoppingToken)
    //    {            
    //        client = new FirebaseClient(fbc);
    //        client.Delete($"{Constants.baseTree}");
    //        _logger.LogInformation("Timed Hosted Service running.");

    //        _timer = new Timer(DoWork, null, TimeSpan.Zero,
    //            TimeSpan.FromSeconds(10));

    //        return Task.CompletedTask;
    //    }

    //    private async void DoWork(object state)
    //    {
    //        await Task.Yield();

    //        using (var scope = _scopeFactory.CreateScope())
    //        {
    //            var dataService = scope.ServiceProvider.GetRequiredService<DeviceDataService>();
    //            await dataService.AddData();
    //        }

    //        var count = Interlocked.Increment(ref executionCount);
    //        _logger.LogInformation(
    //            "Timed Hosted Service is working. Count: {Count}", count);
    //    }

    //    public Task StopAsync(CancellationToken stoppingToken)
    //    {
    //        _logger.LogInformation("Timed Hosted Service is stopping.");

    //        _timer?.Change(Timeout.Infinite, 0);

    //        return Task.CompletedTask;
    //    }

    //    public void Dispose()
    //    {
    //        _timer?.Dispose();
    //    }
    //}
}
