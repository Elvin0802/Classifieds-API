namespace ClassifiedsApp.Application.Interfaces.Services.BackgroundJobs;

public interface IBackgroundJob
{
	Task Execute(CancellationToken cancellationToken);
}