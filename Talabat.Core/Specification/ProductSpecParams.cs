using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specification
{
	public class ProductSpecParams
	{
		private int maxvalue = 10;
		private int pagesize = 5;

		public int Pagesize
		{
			get { return pagesize; }
			set { pagesize = value > maxvalue ? maxvalue : value; }
		}
		public int PageIndex { get; set; } = 1;

        public string? sort { get; set; }
        public int? brandId { get; set; }
        public int? categoryId { get; set; }
		private string? search;

		public string? Search
		{
			get { return search!; }
			set { search = value?.ToLower(); }
		}

	}
}
