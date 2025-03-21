using ClassifiedsApp.Application.Interfaces.Services.AdImage;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;

namespace ClassifiedsApp.Application.Services;

public class AdImageService : IAdImageService
{
	readonly Cloudinary _cloudinary;

	public AdImageService(IConfiguration configuration)
	{
		_cloudinary = new Cloudinary(new Account(configuration["Cloudinary:CloudName"],
												 configuration["Cloudinary:ApiKey"],
												 configuration["Cloudinary:ApiSecret"]));
	}

	public async Task<UploadedAdImage> UploadImage(IFormFile file)
	{
		if (file == null || file.Length == 0)
			throw new ArgumentNullException(nameof(file), "No file uploaded");

		if (!IsImageFile(file))
			throw new UnsupportedContentTypeException("Only image files are allowed");

		using (var stream = file.OpenReadStream())
		{
			var uploadParams = new ImageUploadParams()
			{
				File = new FileDescription(file.FileName, stream),
				UseFilename = true,
				UniqueFilename = true,
				Overwrite = false,

				Transformation = new Transformation()
					.Width(800)
					.Height(600)
					.Crop("limit")
			};

			var uploadResult = await _cloudinary.UploadAsync(uploadParams);

			if (uploadResult.Error != null)
				throw new FileLoadException($"Image upload failed. ex: {uploadResult.Error.Message}");

			return new UploadedAdImage
			{
				PublicId = uploadResult.PublicId,
				Url = uploadResult.SecureUrl.ToString(),
				Format = uploadResult.Format
			};
		}
	}

	public async Task DeleteImage(string publicId)
	{
		if (string.IsNullOrEmpty(publicId))
			throw new ArgumentNullException(nameof(publicId), "Public ID is required");

		var deletionParams = new DeletionParams(publicId);

		var result = await _cloudinary.DestroyAsync(deletionParams);

		if (result.Error != null)
			throw new Exception($"Deletion failed. ex: {result.Error.Message}");
	}

	public bool IsImageFile(IFormFile file)
	{
		var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
		var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

		return !string.IsNullOrEmpty(fileExtension) &&
			   allowedExtensions.Contains(fileExtension) &&
			   file.ContentType.StartsWith("image/");
	}
}

