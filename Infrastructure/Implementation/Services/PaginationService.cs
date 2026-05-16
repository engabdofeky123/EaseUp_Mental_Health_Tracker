using Application.DTO.Pagination;
using Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementation.Services
{
    public class PaginationService : IpaginationService
    {
        public async  Task<PagedResult<T>> GetPagedDataAsync<T>(IQueryable<T> query, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var count = await query.CountAsync(cancellationToken);
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = count,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
        }
    }
}