using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Ads.UpdateAd;

public class UpdateAdCommand : IRequest<UpdateAdCommandResponse>
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public Guid CategoryId { get; set; }
	public Guid LocationId { get; set; }
}
