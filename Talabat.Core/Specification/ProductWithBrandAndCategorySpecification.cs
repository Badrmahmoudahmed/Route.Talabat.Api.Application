using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specification
{
	public class ProductWithBrandAndCategorySpecification :BaseSpecification<Product>
	{
        public ProductWithBrandAndCategorySpecification(ProductSpecParams productSpecParams):base(
                p =>
				 (string.IsNullOrEmpty(productSpecParams.Search) || p.Name.ToLower().Contains(productSpecParams.Search)) &&
                 (!productSpecParams.brandId.HasValue || productSpecParams.brandId ==p.BrandId ) &&
                 (!productSpecParams.categoryId.HasValue || productSpecParams.categoryId == p.CategoryId)
            )
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(p => p.ProductCategory);
            if(!string.IsNullOrEmpty(productSpecParams.sort))
            {
                switch(productSpecParams.sort)
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
            ApplyPagenation((productSpecParams.PageIndex - 1) * productSpecParams.Pagesize, productSpecParams.Pagesize);
        }
        public ProductWithBrandAndCategorySpecification(int id):base(P => P.Id == id)
        {
			Includes.Add(P => P.ProductBrand);
			Includes.Add(p => p.ProductCategory);
		}


    }
}
