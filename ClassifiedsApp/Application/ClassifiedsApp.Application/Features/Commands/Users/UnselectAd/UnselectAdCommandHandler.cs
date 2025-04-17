using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Interfaces.Repositories.Users;
using ClassifiedsApp.Application.Interfaces.Services.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedsApp.Application.Features.Commands.Users.UnselectAd;

public class UnselectAdCommandHandler : IRequestHandler<UnselectAdCommand, Result>
{
	readonly IUserAdSelectionWriteRepository _writeRepository;
	readonly ICurrentUserService _currentUserService;

	public UnselectAdCommandHandler(IUserAdSelectionWriteRepository writeRepository, ICurrentUserService currentUserService)
	{
		_writeRepository = writeRepository;
		_currentUserService = currentUserService;
	}
	public async Task<Result> Handle(UnselectAdCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var item = await _writeRepository.Table.FirstOrDefaultAsync(
								uas => uas.AppUserId == _currentUserService.UserId!.Value && uas.AdId == request.SelectAdId,
								cancellationToken: cancellationToken);

			if (item is null)
				throw new KeyNotFoundException("Ad selection not found with request data.");

			_writeRepository.Remove(item);

			await _writeRepository.SaveAsync();

			return Result.Success("Ad unselected.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error occoured. {ex.Message}");
		}
	}
}
