using System;
using System.Threading.Tasks;
using Hangfire;
using Sisyphus.Core;

namespace Sisyphus.Jobs.HealthCheck
{
    public class HealthCheck : IBackgroundJob, IBackgroundJobSchedule, IBackgroundJobScheduler
    {

        public string GetSchedule()
        {
            return Cron.Minutely();
        }

        public void Schedule()
        {
            RecurringJob.AddOrUpdate<HealthCheck>(
                $"{nameof(HealthCheck)}",
                j => j.RunAsync(),
                GetSchedule());
        }

        public Task RunAsync()
        {
            var seconds = DateTime.Now.Second;

            if (seconds % 5 == 0)
            {
                throw new SisyphusException($"BOOM!!! I blew up at {DateTime.Now}");
            }

            return Task.FromResult("OK");
        }
    }
}