using ClassifiedsApp.Application.Interfaces.Repositories.Users;
using ClassifiedsApp.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedsApp.Application.Features.Commands.Users.SelectAdCommand;

public class SelectAdCommandHandler : IRequestHandler<SelectAdCommand, SelectAdCommandResponse>
{
	readonly IUserAdSelectionWriteRepository _writeRepository;

	public SelectAdCommandHandler(IUserAdSelectionWriteRepository writeRepository)
	{
		_writeRepository = writeRepository;
	}

	public async Task<SelectAdCommandResponse> Handle(SelectAdCommand request, CancellationToken cancellationToken)
	{
		var item = await _writeRepository.Table
							.FirstOrDefaultAsync(
								uas => uas.AppUserId == request.SelectorAppUserId && uas.AdId == request.SelectAdId,
								cancellationToken: cancellationToken);

		if (item is null)
		{
			await _writeRepository.AddAsync(new UserAdSelection()
			{
				AppUserId = request.SelectorAppUserId,
				AdId = request.SelectAdId
			});

			await _writeRepository.SaveAsync();

			return new() { IsSucceeded = true };
		}

		return new() { IsSucceeded = false };
	}
}

