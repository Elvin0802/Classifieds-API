﻿using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Categories.GetAllCategories;

public class GetAllCategoriesQuery : IRequest<GetAllCategoriesQueryResponse>
{
	public int PageNumber { get; set; } = 1;
	public int PageSize { get; set; } = 10;
	public string? SortBy { get; set; }
	public bool IsDescending { get; set; }
}
