namespace System.Linq
{
    using Microsoft.EntityFrameworkCore;
    using Synker.Application.Infrastructure.PagedResult;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public static class PagedResultExtension
    {
        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var pagedResult = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)pagedResult.RowCount / pageSize;
            pagedResult.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            pagedResult.Results = query.Skip(skip).Take(pageSize).ToList();

            return pagedResult;
        }

        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int pageSize, CancellationToken cancellationToken) where T : class
        {
            var pagedResult = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)pagedResult.RowCount / pageSize;
            pagedResult.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            pagedResult.Results = await query.Skip(skip).Take(pageSize).ToListAsync(cancellationToken);

            return pagedResult;
        }

        public static PagedResult<T> GetPaged<T>(this IEnumerable<T> query, int page, int pageSize) where T : class
        {
            var pagedResult = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)pagedResult.RowCount / pageSize;
            pagedResult.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            pagedResult.Results = query.Skip(skip).Take(pageSize).ToList();

            return pagedResult;
        }

        public static PagedResult<M> GetPaged<T, M>(this IEnumerable<T> query, int page, int pageSize, Func<T, M> projection)
            where T : class
            where M : class
        {
            var pagedResult = new PagedResult<M>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)pagedResult.RowCount / pageSize;
            pagedResult.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            pagedResult.Results = query.Skip(skip).Take(pageSize).Select(projection).ToList();

            return pagedResult;
        }
    }
}
