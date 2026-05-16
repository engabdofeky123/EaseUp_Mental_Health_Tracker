using Application.DTO.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IpaginationService
    {
        Task<PagedResult<T>> GetPagedDataAsync<T>(IQueryable<T> query, int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
