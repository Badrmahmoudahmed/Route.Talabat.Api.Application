using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specification
{
	public class ProductWithBrandAndCategorySpecification :BaseSpecification<Product>
	{
        public ProductWithBrandAndCategorySpecification():base()
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(p => p.ProductCategory);
        }

		

	}
}
