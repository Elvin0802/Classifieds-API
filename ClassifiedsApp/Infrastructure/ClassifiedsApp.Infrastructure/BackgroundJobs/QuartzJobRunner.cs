using ClassifiedsApp.Application.Interfaces.Services.BackgroundJobs;
using Quartz;

namespace ClassifiedsApp.Infrastructure.BackgroundJobs;

public class QuartzJobRunner<T> : IJob where T : IBackgroundJob
{
	private readonly T _job;

	public QuartzJobRunner(T job)
	{
		_job = job;
	}

	public async Task Execute(IJobExecutionContext context)
	{
		await _job.Execute(context.CancellationToken);
	}
}