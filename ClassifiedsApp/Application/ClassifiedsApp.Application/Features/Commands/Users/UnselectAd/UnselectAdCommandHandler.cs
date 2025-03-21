using ClassifiedsApp.Application.Interfaces.Repositories.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedsApp.Application.Features.Commands.Users.UnselectAd;

public class UnselectAdCommandHandler : IRequestHandler<UnselectAdCommand, UnselectAdCommandResponse>
{
	readonly IUserAdSelectionWriteRepository _writeRepository;

	public UnselectAdCommandHandler(IUserAdSelectionWriteRepository writeRepository)
	{
		_writeRepository = writeRepository;
	}
	public async Task<UnselectAdCommandResponse> Handle(UnselectAdCommand request, CancellationToken cancellationToken)
	{
		var item = await _writeRepository.Table.FirstOrDefaultAsync(
							uas => uas.AppUserId == request.SelectorAppUserId && uas.AdId == request.SelectAdId,
							cancellationToken: cancellationToken);

		if (item is not null)
		{
			_writeRepository.Remove(item);
			await _writeRepository.SaveAsync();

			return new() { IsSucceeded = true };
		}

		return new() { IsSucceeded = false };
	}
}
