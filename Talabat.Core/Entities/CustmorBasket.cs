using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
	internal class CustmorBasket
	{
		public string Id { get; set; }
        public List<BasketItem> BasketItems { get; set; }
    }


}
