using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Interfaces.Repositories.Locations;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Locations.DeleteLocation;

public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, Result>
{
	readonly ILocationWriteRepository _writeRepository;

	public DeleteLocationCommandHandler(ILocationWriteRepository writeRepository)
	{
		_writeRepository = writeRepository;
	}

	public async Task<Result> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
	{
		try
		{
			if (!await _writeRepository.RemoveAsync(request.Id))
				throw new KeyNotFoundException($"Location with this id: \" {request.Id} \" , was not found.");

			await _writeRepository.SaveAsync();

			return Result.Success("Location deleted successfully.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Location deleting failed. {ex.Message}");
		}
	}
}
