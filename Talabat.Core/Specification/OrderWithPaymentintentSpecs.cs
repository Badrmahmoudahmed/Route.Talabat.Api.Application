using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OredrAggregate;

namespace Talabat.Core.Specification
{
    public class OrderWithPaymentintentSpecs : BaseSpecification<Order>
    {
        public OrderWithPaymentintentSpecs(string paymentintentid) :
           base(O => O.PaymetnIntentId == paymentintentid)
        {

        }
    }
}
