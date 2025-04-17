using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Interfaces.Repositories.Ads;
using ClassifiedsApp.Application.Interfaces.Services.Users;
using ClassifiedsApp.Core.Entities;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Ads.CreateAd;

public class CreateAdCommandHandler : IRequestHandler<CreateAdCommand, Result>
{
	readonly IAdWriteRepository _writeRepository;
	readonly IAdSubCategoryValueWriteRepository _adSubCategoryWriteRepository;
	readonly ICurrentUserService _currentUserService;

	public CreateAdCommandHandler(IAdWriteRepository writeRepository,
								  IAdSubCategoryValueWriteRepository adSubCategoryWriteRepository,
								   ICurrentUserService currentUserService)
	{
		_writeRepository = writeRepository;
		_adSubCategoryWriteRepository = adSubCategoryWriteRepository;
		_currentUserService = currentUserService;
	}

	/*
	
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

	*/

	public async Task<Result> Handle(CreateAdCommand request, CancellationToken cancellationToken)
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
				AppUserId = _currentUserService.UserId!.Value,
				ExpiresAt = DateTimeOffset.MinValue
			};

			////newAd.Status = Core.Enums.AdStatus.Pending; // veziyyeti gozleyen edirik ki , admin tesdiqlesin. // helelik deactive edilib.
			//newAd.Status = Core.Enums.AdStatus.Active; // veziyyeti active edirik ki , tediqlenme olmadan , yoxlaya bilek.

			newAd.Status = Core.Enums.AdStatus.Pending; // veziyyeti gozleyen edirik ki , admin tesdiqlesin.

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

			return Result.Success("Ad created successfully.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error occoured. {ex.Message}");
		}
	}
}
