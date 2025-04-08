using ClassifiedsApp.Core.Enums;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Ads.ChangeAdStatus;

public class ChangeAdStatusCommand : IRequest<ChangeAdStatusCommandResponse>
{
	public Guid AdId { get; set; }
	public AdStatus NewAdStatus { get; set; }
}
