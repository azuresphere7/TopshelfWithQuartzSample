using Quartz;
using Serilog;

namespace TopshelfWithQuartzSample
{
    internal class MyJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var fireTime = context.FireTimeUtc.DateTime.ToString();
            Log.Information("MyJob run: {fireTime}", fireTime);
            return Task.CompletedTask;
        }
    }
}
