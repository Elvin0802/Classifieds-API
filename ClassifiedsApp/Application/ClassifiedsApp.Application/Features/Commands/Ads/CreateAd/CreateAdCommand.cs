using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Dtos.Ads;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Ads.CreateAd;

public class CreateAdCommand : IRequest<Result>
{
	public string Title { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public bool IsNew { get; set; }
	public Guid CategoryId { get; set; }
	public Guid MainCategoryId { get; set; }
	public Guid LocationId { get; set; }

	public IList<CreateAdSubCategoryValueDto> SubCategoryValues { get; set; }

	//public IList<string> ImageUrls { get; set; }
}