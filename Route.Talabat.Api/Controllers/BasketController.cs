using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Api.Dtos;
using Route.Talabat.Api.ErrorsHandler;
using Talabat.Core.Entities;
using Talabat.Core.Repositiry.Contract;

namespace Route.Talabat.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BasketController : ControllerBase
	{
		private readonly IBasketRepository _basketRepository;
		private readonly IMapper _mapper;

		public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
			_basketRepository = basketRepository;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<CustmorBasket>> GetBasket(string id)
		{
			var basket = await _basketRepository.GetBasketAsync(id);
			return Ok(basket ?? new CustmorBasket(id));
		}
		[HttpPost]
		public async Task<ActionResult<CustmorBasket>> UpdateBasket(CustmorBasketDto basket)
		{
			var MappedBasket = _mapper.Map<CustmorBasketDto, CustmorBasket>(basket);
			var UpdatedBasket = await _basketRepository.UpdateBasketAsync(MappedBasket);
			if (UpdatedBasket is null) 
			{
				return BadRequest(new ApiResponse(400));
			}
			return Ok(UpdatedBasket);
		}

		[HttpDelete]
		public async Task<ActionResult<bool>> DeleteBasket(string id)
		{
			return await _basketRepository.DeleteBasketAsync(id);
		}

	}
}
