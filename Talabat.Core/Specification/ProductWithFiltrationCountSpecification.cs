using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specification
{
	public class ProductWithFiltrationCountSpecification : BaseSpecification<Product>
	{
        public ProductWithFiltrationCountSpecification(ProductSpecParams productSpecParams):base(p =>
				 (!productSpecParams.brandId.HasValue || productSpecParams.brandId == p.BrandId) &&
				 (!productSpecParams.categoryId.HasValue || productSpecParams.categoryId == p.CategoryId))
        {
            
        }
    }
}
