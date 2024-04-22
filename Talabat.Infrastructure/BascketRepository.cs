using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositiry.Contract;

namespace Talabat.Infrastructure
{
	public class BascketRepository : IBasketRepository
	{
		private readonly StackExchange.Redis.IDatabase _database;
        public BascketRepository(IConnectionMultiplexer Redis)
        {
            _database = Redis.GetDatabase();
        }
        public Task<bool> DeleteBasketAsync(string BasketId)
		{
			return _database.KeyDeleteAsync(BasketId);
		}

		public async Task<CustmorBasket?> GetBasketAsync(string BasketId)
		{
			var bascket =  await _database.StringGetAsync(BasketId);
			return bascket.IsNull ? null : JsonSerializer.Deserialize<CustmorBasket>(bascket);
		}

		public async Task<CustmorBasket?> UpdateBasketAsync(CustmorBasket basket)
		{
			var UpdatedorCreated = await _database.StringSetAsync(basket.Id , JsonSerializer.Serialize(basket) , TimeSpan.FromDays(30));
			if (UpdatedorCreated is false) return null;
			return await GetBasketAsync(basket.Id);
		}
	}
}
