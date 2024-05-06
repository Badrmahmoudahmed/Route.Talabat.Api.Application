using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OredrAggregate;
using Talabat.Core.Repositiry.Contract;

namespace Talabat.Core
{
	public interface IUnitofWork : IAsyncDisposable
	{
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        Task<int> CompleteAsync();
    }
}
