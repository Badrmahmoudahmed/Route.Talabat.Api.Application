using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositiry.Contract;
using Talabat.Infrastructure.Data;

namespace Talabat.Infrastructure
{
	public class GenericRepositiry<T> : IGenericRepository<T> where T : BaseEntity
	{

		private readonly StoreContext _dbContext;
		public GenericRepositiry(StoreContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<IEnumerable<T>> GetAllAsync()
		{
			if (typeof(T) == typeof(Product))
				return (IEnumerable<T>) await _dbContext.Products.Include(p => p.ProductBrand).Include(p => p.ProductCategory).ToListAsync();
			else
				return await _dbContext.Set<T>().ToListAsync();
		}
		public async Task<T?> GetByIdAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}
	} 
}
