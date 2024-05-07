using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Api.Dtos;
using Route.Talabat.Api.ErrorsHandler;
using Talabat.Core.Entities.OredrAggregate;
using Talabat.Core.Services.Contract;

namespace Route.Talabat.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public OrdersController(IOrderService orderService , IMapper mapper)
        {
			_orderService = orderService;
			_mapper = mapper;
		}

		[HttpPost]
		public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
		{
			//var adress = _mapper.Map<AdressDto, Adress>(orderDto.ShippingAdress);
			var order = await _orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, orderDto.ShippingAdress);
			if (order is null) return BadRequest(new ApiResponse(400));
			return Ok(order);
		}

		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<OrderToReurnDto>>> GetOrdersForUser(string Email)
		{
			var orders = await _orderService.GetOrdersForUserAsync(Email);
			var mappedorders = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReurnDto>>(orders);
			return Ok(mappedorders);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<OrderToReurnDto>> GetOrderForUser(int id , string email)
		{
			var order = await _orderService.GetOrderByIdForUserAsync(email, id);

			if (order is null) return NotFound(new ApiResponse(404));

			var MappdedOrder = _mapper.Map<Order, OrderToReurnDto>(order);
			
			return Ok(MappdedOrder);
		}


	}
}
