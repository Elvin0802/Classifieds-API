using ClassifiedsApp.Infrastructure.Services.BackgroundJobs;
using Quartz;

namespace ClassifiedsApp.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class TestJob : QuartzJobRunner<TestJobService>
{
	public TestJob(TestJobService job) : base(job)
	{
	}
}
