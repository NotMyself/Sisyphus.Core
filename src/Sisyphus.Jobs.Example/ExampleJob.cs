using System;
using System.Threading.Tasks;
using Hangfire;
using Sisyphus.Core;

namespace Sisyphus.Jobs.Example
{
    public class ExampleJob : IBackgroundJob, IBackgroundJobSchedule, IBackgroundJobScheduler
    {

        public string GetSchedule()
        {
            return Cron.Minutely();
        }

        public void Schedule()
        {
            RecurringJob.AddOrUpdate<ExampleJob>(
                $"{nameof(ExampleJob)}",
                j => j.RunAsync(),
                GetSchedule());
        }

        public Task RunAsync()
        {
            Console.WriteLine($"Executing {nameof(ExampleJob)} at {DateTime.Now:d}");

            return Task.FromResult("OK");
        }
    }
}
