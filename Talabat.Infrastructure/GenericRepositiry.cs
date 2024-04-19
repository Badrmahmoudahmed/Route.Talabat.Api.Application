using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositiry.Contract;
using Talabat.Core.Specification;
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
		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			if (typeof(T) == typeof(Product))
				return (IReadOnlyList<T>) await _dbContext.Products.Include(p => p.ProductBrand).Include(p => p.ProductCategory).ToListAsync();
			else
				return await _dbContext.Set<T>().ToListAsync();
		}

		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
		{
			return await ApplySpecification(spec).ToListAsync();
		}

		public async Task<T?> GetByIdAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}

		public async Task<T?> GetByIdWithSpecAsync(ISpecification<T> spec)
		{
			return await ApplySpecification(spec).FirstOrDefaultAsync();
		}

		private  IQueryable<T> ApplySpecification(ISpecification<T> spec)
		{
			return SpecificationEvalutor<T>.GetQuery(_dbContext.Set<T>(), spec);
		}
	} 
}
