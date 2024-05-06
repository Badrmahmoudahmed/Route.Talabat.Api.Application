using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OredrAggregate;
using Talabat.Core.Repositiry.Contract;
using Talabat.Infrastructure.Data;

namespace Talabat.Infrastructure
{
	public class UnitofWork : IUnitofWork
	{
		private readonly StoreContext _dbcontext;

        public Hashtable _repositiory { get; set; }

        public UnitofWork(StoreContext dbcontext)
        {
			_dbcontext = dbcontext;
			_repositiory = new Hashtable();
		}
        public async Task<int> CompleteAsync()
		{
			return await _dbcontext.SaveChangesAsync();
		}

		public async ValueTask DisposeAsync()
		{
			await _dbcontext.DisposeAsync();
		}

		public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
		{
			var key = typeof(TEntity).Name;
			if(!_repositiory.ContainsKey(key))
			{
				var repositiory = new GenericRepositiry<TEntity>(_dbcontext);
				_repositiory.Add(key, repositiory);
			}

			return _repositiory[key] as IGenericRepository<TEntity>;
		}

	
	}
}
