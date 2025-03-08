using ClassifiedsApp.Core.Entities.Common;

namespace ClassifiedsApp.Core.Entities;

public class Category : BaseCategory
{
	public IList<MainCategory> MainCategories { get; set; }
}




/*

// Category Commands and Queries

// Commands

// Queries


    public class GetAdsListQueryHandler : IRequestHandler<GetAdsListQuery, PaginatedList<AdListDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAdsListQueryHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<AdListDto>> Handle(GetAdsListQuery request, CancellationToken cancellationToken)
        {
            // Start with a base query
            var query = _context.Ads
                .Include(a => a.Category)
                .Include(a => a.Location)
                .Include(a => a.Images.Where(i => i.ArchivedAt == null).OrderBy(i => i.SortOrder).Take(1))
                .Where(a => a.ArchivedAt == null);
                
            // Apply status filter, default to active ads only
            if (request.Status.HasValue)
            {
                query = query.Where(a => a.Status == request.Status.Value);
            }
            else
            {
                query = query.Where(a => a.Status == AdStatus.Active);
            }

            // Apply other filters
            if (request.CategoryId.HasValue)
            {
                query = query.Where(a => a.CategoryId == request.CategoryId.Value);
            }

            if (request.LocationId.HasValue)
            {
                query = query.Where(a => a.LocationId == request.LocationId.Value);
            }

            if (request.UserId.HasValue)
            {
                query = query.Where(a => a.AppUserId == request.UserId.Value);
            }

            if (request.MinPrice.HasValue)
            {
                query = query.Where(a => a.Price >= request.MinPrice.Value);
            }

            if (request.MaxPrice.HasValue)
            {
                query = query.Where(a => a.Price <= request.MaxPrice.Value);
            }

            if (request.FeaturedOnly)
            {
                query = query.Where(a => a.IsFeatured);
            }

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                string searchTerm = request.SearchTerm.ToLower();
                query = query.Where(a => 
                    a.Title.ToLower().Contains(


*/