using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Repositiry.Contract
{
	public interface IBasketRepository
	{
		public Task<CustmorBasket?> GetBasketAsync(string BasketId);

		public Task<CustmorBasket?> UpdateBasketAsync(CustmorBasket basket);
		public Task<bool> DeleteBasketAsync(string BasketId);


	}
}
