using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Interfaces.Repositories.Ads;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Ads.CreateAd;

public class CreateAdCommandHandler : IRequestHandler<CreateAdCommand, CreateAdCommandResponse>
{
	readonly IAdWriteRepository _writeRepository;
	readonly IAdSubCategoryValueWriteRepository _adSubCategoryWriteRepository;

	public CreateAdCommandHandler(IAdWriteRepository writeRepository,
								IAdSubCategoryValueWriteRepository adSubCategoryWriteRepository)
	{
		_writeRepository = writeRepository;
		_adSubCategoryWriteRepository = adSubCategoryWriteRepository;
	}

	public async Task<CreateAdCommandResponse> Handle(CreateAdCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Ad newAd = new()
			{
				Title = request.Title.Trim(),
				Description = request.Description.Trim(),
				Price = request.Price,
				IsNew = request.IsNew,
				CategoryId = request.CategoryId,
				MainCategoryId = request.MainCategoryId,
				LocationId = request.LocationId,
				AppUserId = request.AppUserId
			};

			newAd.ExpiresAt = DateTimeOffset.UtcNow.AddDays(7); // elanin saytda qalma muddeti.

			//newAd.Status = Core.Enums.AdStatus.Pending; // veziyyeti gozleyen edirik ki , admin tesdiqlesin. // helelik deactive edilib.
			newAd.Status = Core.Enums.AdStatus.Active; // veziyyeti active edirik ki , tediqlenme olmadan , yoxlaya bilek.

			newAd.SubCategoryValues = new List<AdSubCategoryValue>();

			foreach (var item in request.SubCategoryValues)
			{
				newAd.SubCategoryValues.Add(new()
				{
					AdId = newAd.Id,
					SubCategoryId = item.SubCategoryId,
					Value = item.Value
				});
			}

			newAd.Images = new List<AdImage>();

			// Image service created. use !

			int imageSortOrder = 0;

			List<string> lstImages = [
			"https://upload.wikimedia.org/wikipedia/commons/8/86/BMW_G60_520i_1X7A2443.jpg",
			"https://www.bmw-m.com/content/dam/bmw/marketBMW_M/www_bmw-m_com/all-models/model-navigation/bmw-m340i-xdrive-sedan-flyout-new.png",
			"https://mediapool.bmwgroup.com/cache/P9/202309/P90522951/P90522951-the-bmw-i5-edrive40-driving-10-2023-2247px.jpg"];

			foreach (var i in lstImages)
				newAd.Images.Add(new()
				{
					AdId = newAd.Id,
					Url = i,
					SortOrder = imageSortOrder++
				});

			await _writeRepository.AddAsync(newAd);
			await _adSubCategoryWriteRepository.AddRangeAsync(newAd.SubCategoryValues.ToList());
			await _writeRepository.SaveAsync();

			return new()
			{
				IsSucceeded = true,
				Message = "Ad created."
			};
		}
		catch (Exception ex)
		{
			return new()
			{
				IsSucceeded = false,
				Message = $"Ad creating failed. {ex.Message}"
			};
		}
	}
}

//using CloudinaryDotNet;
//using CloudinaryDotNet.Actions;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.IO;
//using System.Threading.Tasks;

//namespace YourApi.Controllers
//{
//	[Route("api/[controller]")]
//	[ApiController]
//	public class CloudinaryController : ControllerBase
//	{
//		private readonly Cloudinary _cloudinary;

//		public CloudinaryController(IConfiguration configuration)
//		{
//			// Get Cloudinary configuration from appsettings.json
//			var cloudinaryAccount = new Account(
//				configuration["Cloudinary:CloudName"],
//				configuration["Cloudinary:ApiKey"],
//				configuration["Cloudinary:ApiSecret"]);

//			_cloudinary = new Cloudinary(cloudinaryAccount);
//		}

//		[HttpPost("upload")]
//		public async Task<IActionResult> UploadImage(IFormFile file)
//		{
//			if (file == null || file.Length == 0)
//				return BadRequest("No file uploaded");

//			try
//			{
//				// Validate file type if needed
//				if (!IsImageFile(file))
//					return BadRequest("Only image files are allowed");

//				using (var stream = file.OpenReadStream())
//				{
//					// Create upload parameters
//					var uploadParams = new ImageUploadParams()
//					{
//						File = new FileDescription(file.FileName, stream),
//						UseFilename = true,
//						UniqueFilename = true,
//						Overwrite = false,
//						// Optional: Add transformation to resize or crop the image
//						Transformation = new Transformation()
//							.Width(800)
//							.Height(600)
//							.Crop("limit")
//					};

//					// Upload to Cloudinary
//					var uploadResult = await _cloudinary.UploadAsync(uploadParams);

//					// Check if upload was successful
//					if (uploadResult.Error != null)
//					{
//						return StatusCode(500, $"Upload failed: {uploadResult.Error.Message}");
//					}

//					// Return the result with URLs and other information
//					return Ok(new
//					{
//						PublicId = uploadResult.PublicId,
//						Url = uploadResult.SecureUrl.ToString(),
//						Format = uploadResult.Format,
//						Width = uploadResult.Width,
//						Height = uploadResult.Height
//					});
//				}
//			}
//			catch (Exception ex)
//			{
//				return StatusCode(500, $"Internal server error: {ex.Message}");
//			}
//		}

//		[HttpPost("upload-base64")]
//		public async Task<IActionResult> UploadBase64Image([FromBody] Base64ImageUploadModel model)
//		{
//			if (string.IsNullOrEmpty(model.Base64Image))
//				return BadRequest("No image data provided");

//			try
//			{
//				// Create upload parameters for base64 image
//				var uploadParams = new ImageUploadParams()
//				{
//					File = new FileDescription($"data:image/png;base64,{model.Base64Image}"),
//					UseFilename = false,
//					UniqueFilename = true,
//					Folder = model.Folder ?? "uploads"
//				};

//				// Upload to Cloudinary
//				var uploadResult = await _cloudinary.UploadAsync(uploadParams);

//				// Check if upload was successful
//				if (uploadResult.Error != null)
//				{
//					return StatusCode(500, $"Upload failed: {uploadResult.Error.Message}");
//				}

//				// Return the result
//				return Ok(new
//				{
//					PublicId = uploadResult.PublicId,
//					Url = uploadResult.SecureUrl.ToString(),
//					Format = uploadResult.Format,
//					Width = uploadResult.Width,
//					Height = uploadResult.Height
//				});
//			}
//			catch (Exception ex)
//			{
//				return StatusCode(500, $"Internal server error: {ex.Message}");
//			}
//		}

//		[HttpDelete("delete/{publicId}")]
//		public async Task<IActionResult> DeleteImage(string publicId)
//		{
//			if (string.IsNullOrEmpty(publicId))
//				return BadRequest("Public ID is required");

//			try
//			{
//				// Create deletion parameters
//				var deletionParams = new DeletionParams(publicId);

//				// Delete from Cloudinary
//				var result = await _cloudinary.DestroyAsync(deletionParams);

//				// Check if deletion was successful
//				if (result.Error != null)
//				{
//					return StatusCode(500, $"Deletion failed: {result.Error.Message}");
//				}

//				return Ok(new { Message = "Image deleted successfully", Result = result.Result });
//			}
//			catch (Exception ex)
//			{
//				return StatusCode(500, $"Internal server error: {ex.Message}");
//			}
//		}

//		private bool IsImageFile(IFormFile file)
//		{
//			// Check file extension and/or mime type
//			var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
//			var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

//			return !string.IsNullOrEmpty(fileExtension) &&
//				   allowedExtensions.Contains(fileExtension) &&
//				   file.ContentType.StartsWith("image/");
//		}
//	}

//	public class Base64ImageUploadModel
//	{
//		public string Base64Image { get; set; }
//		public string Folder { get; set; }
//	}
//}






//using CloudinaryDotNet;
//using CloudinaryDotNet.Actions;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace YourApp.Features.Products
//{
//	// Models
//	public class Product
//	{
//		public int Id { get; set; }
//		public string Name { get; set; }
//		public decimal Price { get; set; }
//		public string Description { get; set; }
//		public ICollection<PImage> ProductImages { get; set; } = new List<PImage>();
//	}

//	public class PImage
//	{
//		public int Id { get; set; }
//		public string Url { get; set; }
//		public int ProductId { get; set; }
//		public Product Product { get; set; }
//	}

//	// Command Request
//	public class CreateProductCommand : IRequest<CreateProductResponse>
//	{
//		public string Name { get; set; }
//		public decimal Price { get; set; }
//		public string Description { get; set; }
//		public List<IFormFile> ImageFiles { get; set; }
//	}

//	public class ProductImageDto
//	{
//		public int Id { get; set; }
//		public string Url { get; set; }
//	}

//	public class CreateProductResponse
//	{
//		public int ProductId { get; set; }
//		public string Name { get; set; }
//		public decimal Price { get; set; }
//		public string Description { get; set; }
//		public List<ProductImageDto> Images { get; set; } = new List<ProductImageDto>();
//	}

//	// Command Handler
//	public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
//	{
//		private readonly IProductRepository _productRepository;
//		private readonly Cloudinary _cloudinary;

//		public CreateProductCommandHandler(
//			IProductRepository productRepository,
//			IConfiguration configuration)
//		{
//			_productRepository = productRepository;

//			// Initialize Cloudinary
//			var cloudinaryAccount = new Account(
//				configuration["Cloudinary:CloudName"],
//				configuration["Cloudinary:ApiKey"],
//				configuration["Cloudinary:ApiSecret"]);

//			_cloudinary = new Cloudinary(cloudinaryAccount);
//		}

//		public async Task<CreateProductResponse> Handle(
//			CreateProductCommand request,
//			CancellationToken cancellationToken)
//		{
//			// 1. Create Product entity first (without images)
//			var product = new Product
//			{
//				Name = request.Name,
//				Price = request.Price,
//				Description = request.Description,
//				ProductImages = new List<PImage>()
//			};

//			// 2. Upload all images to Cloudinary and link them to the product
//			if (request.ImageFiles != null && request.ImageFiles.Any())
//			{
//				foreach (var imageFile in request.ImageFiles)
//				{
//					if (imageFile.Length > 0)
//					{
//						string imageUrl = await UploadImageToCloudinary(imageFile);

//						if (!string.IsNullOrEmpty(imageUrl))
//						{
//							product.ProductImages.Add(new PImage { Url = imageUrl });
//						}
//					}
//				}
//			}

//			// 3. Save to database
//			await _productRepository.AddProductAsync(product, cancellationToken);

//			// 4. Return response
//			return new CreateProductResponse
//			{
//				ProductId = product.Id,
//				Name = product.Name,
//				Price = product.Price,
//				Description = product.Description,
//				Images = product.ProductImages.Select(img => new ProductImageDto
//				{
//					Id = img.Id,
//					Url = img.Url
//				}).ToList()
//			};
//		}

//		private async Task<string> UploadImageToCloudinary(IFormFile imageFile)
//		{
//			try
//			{
//				using (var stream = imageFile.OpenReadStream())
//				{
//					var uploadParams = new ImageUploadParams
//					{
//						File = new FileDescription(imageFile.FileName, stream),
//						Folder = "products",
//						UseFilename = true,
//						UniqueFilename = true
//					};

//					var uploadResult = await _cloudinary.UploadAsync(uploadParams);
//					if (uploadResult.Error != null)
//					{
//						throw new ApplicationException($"Failed to upload image: {uploadResult.Error.Message}");
//					}

//					return uploadResult.SecureUrl.ToString();
//				}
//			}
//			catch (Exception ex)
//			{
//				// Log error
//				Console.WriteLine($"Error uploading image: {ex.Message}");
//				return null;
//			}
//		}
//	}

//	// Repository Interface
//	public interface IProductRepository
//	{
//		Task AddProductAsync(Product product, CancellationToken cancellationToken);
//	}
//}