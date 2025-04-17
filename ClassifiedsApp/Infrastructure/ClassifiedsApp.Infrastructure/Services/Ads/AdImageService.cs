using ClassifiedsApp.Application.Interfaces.Services.AdImage;
using Microsoft.AspNetCore.Http;

namespace ClassifiedsApp.Application.Services;

public class AdImageService : IAdImageService
{
	public Task DeleteImage(string publicId)
	{
		throw new NotImplementedException();
	}

	public bool IsImageFile(IFormFile file)
	{
		throw new NotImplementedException();
	}

	public Task<UploadedAdImage> UploadImage(IFormFile file)
	{
		throw new NotImplementedException();
	}
}

