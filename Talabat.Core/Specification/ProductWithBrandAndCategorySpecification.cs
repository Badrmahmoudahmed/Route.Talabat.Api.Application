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
        public ProductWithBrandAndCategorySpecification(string? sort):base()
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(p => p.ProductCategory);
            if(!string.IsNullOrEmpty(sort))
            {
                switch(sort)
                {
                    case "priceAsc":
                        OrderBy = p => p.Price; 
                        break;
                    case "priceDesc":
                        OrderByDescndig = p => p.Price;
                        break;
                    default: 
                        OrderBy = p => p.Name;
                        break;
                }
            }
            else
            {
                OrderBy = p => p.Name;
            }
        }
        public ProductWithBrandAndCategorySpecification(int id):base(P => P.Id == id)
        {
			Includes.Add(P => P.ProductBrand);
			Includes.Add(p => p.ProductCategory);
		}


    }
}
