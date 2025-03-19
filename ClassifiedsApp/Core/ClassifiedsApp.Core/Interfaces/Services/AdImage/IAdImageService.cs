using Microsoft.AspNetCore.Http;

namespace ClassifiedsApp.Core.Interfaces.Services.AdImage;

public interface IAdImageService
{
	Task<UploadedAdImage> UploadImage(IFormFile file);
	Task DeleteImage(string publicId);
	bool IsImageFile(IFormFile file);
}


public class UploadedAdImage
{
	public string PublicId { get; set; }
	public string Url { get; set; }
	public string Format { get; set; }
}