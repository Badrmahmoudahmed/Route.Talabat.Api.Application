using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
	internal class Product : BaseEntity
	{
        public String Name { get; set; }
        public string Discription { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public int CategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}
