using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specification;

namespace Talabat.Infrastructure
{
	internal class SpecificationEvalutor<T> where T : BaseEntity
	{
		public static IQueryable<T> GetQuery(IQueryable<T> InputQuery , ISpecification<T> Spec)
		{
			var query = InputQuery;

			if (Spec.Criteria is not null)
				query = InputQuery.Where(Spec.Criteria);

			query = Spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

			return query;
		}
	}
}
