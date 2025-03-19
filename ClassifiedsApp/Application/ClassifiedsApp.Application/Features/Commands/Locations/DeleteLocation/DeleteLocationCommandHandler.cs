using ClassifiedsApp.Core.Interfaces.Repositories.Locations;
using ClassifiedsApp.Core.Interfaces.Services.Cache;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Locations.DeleteLocation;

public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, DeleteLocationCommandResponse>
{
	readonly ILocationWriteRepository _writeRepository;
	readonly ICacheService _cacheService;

	public DeleteLocationCommandHandler(ILocationWriteRepository writeRepository,
										ICacheService cacheService)
	{
		_writeRepository = writeRepository;
		_cacheService = cacheService;
	}

	public async Task<DeleteLocationCommandResponse> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
	{
		try
		{
			if (!await _writeRepository.RemoveAsync(request.Id))
				throw new KeyNotFoundException($"Location with this id: \" {request.Id} \" , was not found.");

			await _writeRepository.SaveAsync();

			await _cacheService.RemoveByPrefixAsync("locations_");

			return new()
			{
				IsSucceeded = true,
				Message = $"Location deleted."
			};
		}
		catch (Exception ex)
		{
			return new()
			{
				IsSucceeded = false,
				Message = $"Location deleting failed. {ex.Message}"
			};
		}
	}
}
