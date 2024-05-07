using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OredrAggregate;

namespace Talabat.Core.Specification
{
	public class OrderSpecs : BaseSpecification<Order>
	{
        public OrderSpecs(string Email): base(o => o.BuyerEmail == Email)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);

            OrderByDescndig = o => o.OrderDate;
        }

		public OrderSpecs(int id , string Email) : base(o => o.Id == id && o.BuyerEmail == Email) 
			
		{
			Includes.Add(o => o.DeliveryMethod);
			Includes.Add(o => o.Items);

		}
	}
}
