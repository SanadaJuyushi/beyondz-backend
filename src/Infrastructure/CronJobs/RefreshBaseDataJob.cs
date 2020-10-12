using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.CronJobs
{
    public class RefreshBaseDataJob : CronJobService
    {
        private readonly ILogger<RefreshBaseDataJob> _logger;
        public IServiceProvider Services { get; }

        public RefreshBaseDataJob(IScheduleConfig<RefreshBaseDataJob> config, ILogger<RefreshBaseDataJob> logger, IServiceProvider services)
        : base(config.CronExpression, config.TimeZoneInfo)
        {
            Services = services;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("RefreshBaseDataJob starts.");
            return base.StartAsync(cancellationToken);
        }

        public override Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} RefreshBaseDataJob is working.");

            using (var scope = Services.CreateScope())
            {
                var skillTagService =
                    scope.ServiceProvider
                        .GetRequiredService<ISkillTagService>();

                skillTagService.GeneratorStackOverflowTags();
            }

            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("RefreshBaseDataJob is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
