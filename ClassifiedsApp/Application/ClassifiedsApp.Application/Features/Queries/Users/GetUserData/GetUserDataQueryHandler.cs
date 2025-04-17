using AutoMapper;
using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Dtos.Auth.Users;
using ClassifiedsApp.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ClassifiedsApp.Application.Features.Queries.Users.GetUserData;

public class GetUserDataQueryHandler : IRequestHandler<GetUserDataQuery, Result<GetUserDataQueryResponse>>
{
	readonly UserManager<AppUser> _userManager;
	readonly IMapper _mapper;

	public GetUserDataQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
	{
		_userManager = userManager;
		_mapper = mapper;
	}

	public async Task<Result<GetUserDataQueryResponse>> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var user = _mapper.Map<AppUserDto>(await _userManager.FindByIdAsync(request.Id.ToString()!));

			return user is null
					? throw new KeyNotFoundException("User not found.")
					: Result.Success(new GetUserDataQueryResponse() { Item = user }, "User retrieved successfully.");
		}
		catch (Exception ex)
		{
			return Result.Failure<GetUserDataQueryResponse>($"Failed to retrieve user: {ex.Message}");
		}
	}
}
