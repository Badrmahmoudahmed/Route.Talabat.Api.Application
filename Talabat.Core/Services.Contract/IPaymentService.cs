using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OredrAggregate;

namespace Talabat.Core.Services.Contract
{
    public interface IPaymentService
    {
        Task<CustmorBasket> CreateorUpdatePaymentIntent(string basketId);
        Task<Order?> UpdateOrderStatus(string paymentintentid, bool ispaid);
    }
}
