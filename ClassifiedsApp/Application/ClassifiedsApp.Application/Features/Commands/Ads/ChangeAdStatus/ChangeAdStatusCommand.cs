using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Core.Enums;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Ads.ChangeAdStatus;

public class ChangeAdStatusCommand : IRequest<Result>
{
	public Guid AdId { get; set; }
	public AdStatus NewAdStatus { get; set; }
}
