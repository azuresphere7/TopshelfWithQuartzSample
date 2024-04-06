using Quartz;
using Quartz.Impl;

namespace TopshelfWithQuartzSample
{
    internal class MyService
    {
        private readonly IScheduler scheduler;

        public MyService()
        {
            var factory = new StdSchedulerFactory();
            scheduler = factory.GetScheduler().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public void Start() 
        {
            scheduler.Start().ConfigureAwait(false).GetAwaiter().GetResult();

            IJobDetail job = JobBuilder.Create<MyJob>()
            .WithIdentity("Myjob")
            .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("MyTrigger")
                .StartNow()
                .WithSimpleSchedule(scheduleBuilder => scheduleBuilder
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        public void Stop() 
        {
            scheduler.Shutdown().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
