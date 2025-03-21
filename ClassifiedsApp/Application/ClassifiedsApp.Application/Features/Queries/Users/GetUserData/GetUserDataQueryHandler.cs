using AutoMapper;
using ClassifiedsApp.Application.Dtos.Auth.Users;
using ClassifiedsApp.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ClassifiedsApp.Application.Features.Queries.Users.GetUserData;

public class GetUserDataQueryHandler : IRequestHandler<GetUserDataQuery, GetUserDataQueryResponse>
{
	readonly UserManager<AppUser> _userManager;
	readonly IMapper _mapper;

	public GetUserDataQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
	{
		_userManager = userManager;
		_mapper = mapper;
	}

	public async Task<GetUserDataQueryResponse> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
	{
		var user = await _userManager.FindByIdAsync(request.AppUserId.ToString()!);

		if (user is null)
			throw new KeyNotFoundException(nameof(user));

		return new()
		{
			AppUserDto = _mapper.Map<AppUserDto>(user)
		};
	}
}
