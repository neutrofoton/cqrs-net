using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Core.Queries;

namespace CQRS.Core.Queries.Infrastructures
{
    public interface IQueryDispatcher<TEntity>
    {
        void RegisterHandler<TQuery>(Func<TQuery, Task<List<TEntity>>> handler) where TQuery : BaseQuery;
        Task<List<TEntity>> SendAsync(BaseQuery query);
    }
}